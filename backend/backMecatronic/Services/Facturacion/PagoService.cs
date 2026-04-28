using backMecatronic.Data;
using backMecatronic.Models.DTOs.Facturacion;
using backMecatronic.Models.Entities.Facturacion;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Facturacion
{
    public class PagoService : IPagoService
    {
        private readonly AppDbContext _context;

        public PagoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PagoResponseDto>> ObtenerPagos()
        {
            return await _context.Pago
                .Include(p => p.MetodoPago)
                .Select(p => new PagoResponseDto
                {
                    IdPago = p.IdPago,
                    IdOrdenServicio = p.IdOrdenServicio,
                    NombreMetodoPago = p.MetodoPago.NombreMetodoPago,
                    Monto = p.Monto,
                    FechaPago = p.FechaPago
                })
                .ToListAsync();
        }

        public async Task<PagoResponseDto> ObtenerPagoPorId(int id)
        {
            var pago = await _context.Pago
                .Include(p => p.MetodoPago)
                .FirstOrDefaultAsync(p => p.IdPago == id);
            if (pago == null)
                throw new Exception("Pago no encontrado");
            return new PagoResponseDto
            {
                IdPago = pago.IdPago,
                IdOrdenServicio = pago.IdOrdenServicio,
                NombreMetodoPago = pago.MetodoPago.NombreMetodoPago,
                Monto = pago.Monto,
                FechaPago = pago.FechaPago
            };
        }

        public async Task<PagoResponseDto> RegistrarPago(PagoCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var pedido = await _context.OrdenServicio
                    .FirstOrDefaultAsync(p => p.IdPedido == dto.IdOrdenServicio);

                if (pedido == null)
                    throw new Exception("El pedido no existe");

                if (pedido.Estado == "Pagado")
                    throw new Exception("El pedido ya está pagado");

                var metodo = await _context.MetodoPago
                    .FirstOrDefaultAsync(m => m.IdMetodoPago == dto.IdMetodoPago);

                if (metodo == null)
                    throw new Exception("Método de pago inválido");

                if (dto.Monto <= 0)
                    throw new Exception("Monto inválido");

                // Validación simple (puedes mejorarla luego con pagos parciales)
                if (dto.Monto < pedido.Total)
                    throw new Exception("El monto no cubre el total del pedido");

                var pago = new Pago
                {
                    IdOrdenServicio = dto.IdOrdenServicio,
                    IdMetodoPago = dto.IdMetodoPago,
                    Monto = dto.Monto,
                    FechaPago = DateTime.Now
                };

                _context.Pago.Add(pago);

                // 🔥 Actualizar estado del pedido
                pedido.Estado = "Pagado";

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new PagoResponseDto
                {
                    IdPago = pago.IdPago,
                    IdOrdenServicio = pago.IdOrdenServicio,
                    NombreMetodoPago = pago.MetodoPago.NombreMetodoPago,
                    Monto = pago.Monto,
                    FechaPago = pago.FechaPago
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarPago(int id, PagoUpdateDto dto)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
                throw new Exception("Pago no encontrado");
            var metodo = await _context.MetodoPago
                .FirstOrDefaultAsync(m => m.IdMetodoPago == dto.IdMetodoPago);
            if (metodo == null)
                throw new Exception("Método de pago inválido");
            if (dto.Monto <= 0)
                throw new Exception("Monto inválido");
            pago.IdOrdenServicio = dto.IdOrdenServicio;
            pago.IdMetodoPago = dto.IdMetodoPago;
            pago.Monto = dto.Monto;
            _context.Pago.Update(pago);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPago(int id)
        {
            var pago = await _context.Pago.FindAsync(id);
            if (pago == null)
                throw new Exception("Pago no encontrado");
            _context.Pago.Remove(pago);
            await _context.SaveChangesAsync();
            return true;
        }

        // Métodos para métodos de pago
        public async Task<List<MetodoPagoResponseDto>> ObtenerMetodosPago()
        {
            return await _context.MetodoPago
                .Select(m => new MetodoPagoResponseDto
                {
                    IdMetodoPago = m.IdMetodoPago,
                    NombreMetodoPago = m.NombreMetodoPago,
                    DescripcionMetodoPago = m.DescripcionMetodoPago,
                    EstadoMetodoPago = m.EstadoMetodoPago
                })
                .ToListAsync();
        }
    }
}
