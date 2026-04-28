using backMecatronic.Data;
using backMecatronic.Models.DTOs.External;
using backMecatronic.Models.Entities.Facturacion;
using backMecatronic.Services.External;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Facturacion
{
    public class ComprobanteService : IComprobanteService
    {
        private readonly AppDbContext _context;
        private readonly INubeFactService _nubeFact;

        public ComprobanteService(AppDbContext context, INubeFactService nubeFact)
        {
            _context = context;
            _nubeFact = nubeFact;
        }

        public async Task<Comprobante> GenerarComprobante(int idOrden, string serie, int tipoComprobante)
        {
            var orden = await _context.OrdenServicio
                .Include(o => o.Cliente)
                .Include(o => o.Detalles)
                .FirstOrDefaultAsync(o => o.IdOrdenServicio == idOrden);

            if (orden == null)
                throw new Exception("Orden no encontrada");

            // Correlativo real
            var ultimo = await _context.Comprobante
                .Where(c => c.Serie == serie)
                .MaxAsync(c => (int?)c.Numero) ?? 0;

            int numero = ultimo + 1;

            // Cálculos
            var total = orden.Total;
            var totalGravada = total / 1.18m;
            var igv = total - totalGravada;

            // Request a NubeFact
            var request = new NubeFactRequestDto
            {
                TipoComprobante = tipoComprobante,
                Serie = serie,
                Numero = numero,
                ClienteTipoDocumento = orden.Cliente!.IdTipoDocumento,
                ClienteNumeroDocumento = orden.Cliente.NumeroDocumentoCliente,
                ClienteDenominacion = $"{orden.Cliente.NombreCliente} {orden.Cliente.ApellidoCliente}",
                FechaEmision = DateTime.Now.ToString("dd-MM-yyyy"),
                Moneda = 1,
                PorcentajeIgv = 18,
                Total = total,
                TotalGravada = totalGravada,
                TotalIgv = igv,
                EnviadoSunat = true,
                Items = orden.Detalles.Select(d =>
                {
                    decimal precioUnitario = d.PrecioUnitario;
                    decimal valorUnitario = precioUnitario / 1.18m;

                    decimal totalLinea = d.Cantidad * precioUnitario;
                    decimal subtotalLinea = totalLinea / 1.18m;
                    decimal igvLinea = totalLinea - subtotalLinea;

                    return new NubeFactItemDto
                    {
                        UnidadDeMedida = "NIU",
                        Descripcion = d.Descripcion ?? "Producto",
                        Cantidad = d.Cantidad,
                        ValorUnitario = Math.Round(valorUnitario, 10),
                        PrecioUnitario = Math.Round(precioUnitario, 10),
                        Subtotal = Math.Round(subtotalLinea, 2),
                        TipoDeIgv = 1,
                        Igv = Math.Round(igvLinea, 2),
                        Total = Math.Round(totalLinea, 2)
                    };
                }).ToList()
            };

            // Enviar
            var response = await _nubeFact.Enviar(request);

            // Guardar
            var comprobante = new Comprobante(
                orden.IdOrdenServicio,
                tipoComprobante,
                serie,
                numero,
                request.ClienteTipoDocumento,
                request.ClienteNumeroDocumento,
                request.ClienteDenominacion,
                request.Moneda,
                totalGravada,
                igv,
                total,
                response.Aceptada
            );

            comprobante.Enlace = response.Enlace;
            comprobante.EnlacePdf = response.Pdf;
            comprobante.EnlaceXml = response.Xml;

            _context.Comprobante.Add(comprobante);
            await _context.SaveChangesAsync();

            return comprobante;
        }
    }
}
