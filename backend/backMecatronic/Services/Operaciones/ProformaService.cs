using backMecatronic.Data;
using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Models.Entities.Cotizacion;
using backMecatronic.Models.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Operaciones
{
    public class ProformaService : IProformaService
    {
        private readonly AppDbContext _context;

        public ProformaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProformaResponseDto>> ObtenerProformas()
        {
            return await _context.Proforma
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Select(p => new ProformaResponseDto
                {
                    IdProforma = p.IdProforma,
                    NombreCliente = p.Cliente.NombreCliente,
                    FechaEmision = p.FechaEmision,
                    FechaVencimiento = p.FechaVencimiento,
                    Total = p.Total,
                    Detalles = p.Detalles.Select(d => new DetalleProformaResponseDto
                    {
                        IdDetalleProforma = d.IdDetalleProforma,
                        NombreProducto = d.Producto != null ? d.Producto.NombreProducto : null,
                        NombreServicio = d.Servicio != null ? d.Servicio.NombreServicio : null,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        SubTotal = d.SubTotal
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ProformaResponseDto?> ObtenerProformaPorId(int id)
        {
            return await _context.Proforma
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Where(p => p.IdProforma == id)
                .Select(p => new ProformaResponseDto
                {
                    IdProforma = p.IdProforma,
                    NombreCliente = p.Cliente.NombreCliente,
                    FechaEmision = p.FechaEmision,
                    FechaVencimiento = p.FechaVencimiento,
                    Total = p.Total,
                    Detalles = p.Detalles.Select(d => new DetalleProformaResponseDto
                    {
                        IdDetalleProforma = d.IdDetalleProforma,
                        NombreProducto = d.Producto != null ? d.Producto.NombreProducto : null,
                        NombreServicio = d.Servicio != null ? d.Servicio.NombreServicio : null,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        SubTotal = d.SubTotal
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProformaResponseDto> CrearProforma(ProformaCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Cliente
                    .FindAsync(dto.IdCliente);

                if (cliente == null)
                    throw new Exception("Cliente no encontrado");

                decimal total = 0;

                var detalles = new List<DetalleProforma>();

                foreach (var detalleDto in dto.Detalles)
                {
                    decimal precioUnitario = 0;
                    if (detalleDto.IdProducto.HasValue)
                    {
                        var producto = await _context.Producto
                            .FindAsync(detalleDto.IdProducto.Value);
                        if (producto == null)
                            throw new Exception("Producto no encontrado");
                        precioUnitario = producto.PrecioProducto;
                    }
                    else if (detalleDto.IdServicio.HasValue)
                    {
                        var servicio = await _context.Servicio
                            .FindAsync(detalleDto.IdServicio.Value);
                        if (servicio == null)
                            throw new Exception("Servicio no encontrado");
                        precioUnitario = servicio.PrecioServicio;
                    }
                    else
                    {
                        throw new Exception("Detalle debe tener un producto o servicio");
                    }
                    decimal subTotal = precioUnitario * detalleDto.Cantidad;
                    total += subTotal;
                    detalles.Add(new DetalleProforma
                    {
                        IdProducto = detalleDto.IdProducto,
                        IdServicio = detalleDto.IdServicio,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = precioUnitario,
                        SubTotal = subTotal
                    });
                }

                var proforma = new Proforma
                {
                    IdCliente = dto.IdCliente,
                    FechaEmision = DateTime.Now,
                    FechaVencimiento = dto.FechaVencimiento,
                    Total = total,
                    Detalles = detalles
                };

                _context.Proforma.Add(proforma);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ProformaResponseDto
                {
                    IdProforma = proforma.IdProforma,
                    NombreCliente = cliente.NombreCliente,
                    FechaEmision = proforma.FechaEmision,
                    FechaVencimiento = proforma.FechaVencimiento,
                    Total = proforma.Total,
                    Detalles = proforma.Detalles.Select(d => new DetalleProformaResponseDto
                    {
                        IdDetalleProforma = d.IdDetalleProforma,
                        NombreProducto = d.Producto?.NombreProducto,
                        NombreServicio = d.Servicio?.NombreServicio,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        SubTotal = d.SubTotal
                    }).ToList()
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarProforma(int id, ProformaUpdateDto dto)
        {
            var proforma = await _context.Proforma.FindAsync(id);
            if (proforma == null)
                return false;
            var cliente = await _context.Cliente.FindAsync(dto.IdCliente);
            if (cliente == null)
                throw new Exception("Cliente no encontrado");
            proforma.IdCliente = dto.IdCliente;
            proforma.FechaVencimiento = dto.FechaVencimiento;
            _context.Proforma.Update(proforma);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProforma(int id)
        {
            var proforma = await _context.Proforma.FindAsync(id);
            if (proforma == null)
                return false;
            _context.Proforma.Remove(proforma);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
