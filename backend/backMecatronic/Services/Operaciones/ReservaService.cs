using backMecatronic.Data;
using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Models.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Operaciones
{
    public class ReservaService : IReservaService
    {
        private readonly AppDbContext _context;

        public ReservaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservaResponseDto>> ObtenerReservas()
        {
            var reservas = await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Select(r => new ReservaResponseDto
                {
                    IdReserva = r.IdReserva,
                    NombreCliente = r.Cliente.NombreCliente,
                    FechaProgramada = r.FechaProgramada,
                    Estado = r.Estado,
                    Detalles = r.Detalles.Select(d => new DetalleReservaResponseDto
                    {
                        NombreServicio = d.Servicio.NombreServicio,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Cantidad * d.PrecioUnitario,
                        Observaciones = d.Observaciones
                    }).ToList()
                })
                .ToListAsync();
            return reservas;
        }

        public async Task<ReservaResponseDto?> ObtenerReservaPorId(int id)
        {
            return await _context.Reserva
                .Include(r => r.Cliente)
                .Include(r => r.Detalles)
                    .ThenInclude(d => d.Servicio)
                .Select(r => new ReservaResponseDto
                {
                    IdReserva = r.IdReserva,
                    NombreCliente = r.Cliente.NombreCliente,
                    FechaProgramada = r.FechaProgramada,
                    Estado = r.Estado,
                    Detalles = r.Detalles.Select(d => new DetalleReservaResponseDto
                    {
                        NombreServicio = d.Servicio.NombreServicio,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Cantidad * d.PrecioUnitario,
                        Observaciones = d.Observaciones
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ReservaResponseDto> CrearReserva(ReservaCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.IdCliente == dto.IdCliente);

                if (cliente == null)
                    throw new Exception("Cliente no encontrado");

                decimal total = 0;

                var detalles = new List<DetalleReservaServicio>();

                foreach (var detalleDto in dto.Detalles)
                {
                    var servicio = await _context.Servicio
                        .FirstOrDefaultAsync(s => s.IdServicio == detalleDto.IdServicio);

                    if (servicio == null)
                        throw new Exception($"Servicio con ID {detalleDto.IdServicio} no encontrado");

                    var subtotal = detalleDto.Cantidad * servicio.PrecioServicio;
                    total += subtotal;

                    detalles.Add(new DetalleReservaServicio
                    {
                        IdServicio = detalleDto.IdServicio,
                        Cantidad = detalleDto.Cantidad,
                        PrecioUnitario = servicio.PrecioServicio,
                        Observaciones = detalleDto.Observaciones
                    });
                }

                var reserva = new Reserva
                {
                    IdCliente = dto.IdCliente,
                    FechaProgramada = dto.FechaProgramada,
                    Estado = "Pendiente",
                    //Total = total,
                    Detalles = detalles
                };

                _context.Reserva.Add(reserva);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ReservaResponseDto
                {
                    IdReserva = reserva.IdReserva,
                    NombreCliente = cliente.NombreCliente,
                    FechaProgramada = reserva.FechaProgramada,
                    Estado = reserva.Estado,
                    Detalles = reserva.Detalles.Select(d => new DetalleReservaResponseDto
                    {
                        NombreServicio = d.Servicio.NombreServicio,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Cantidad * d.PrecioUnitario,
                        Observaciones = d.Observaciones
                    }).ToList()
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarReserva(int id, ReservaUpdateDto dto)
        {
            var reserva = await _context.Reserva.FindAsync(id);

            if (reserva == null)
                return false;

            reserva.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarReserva(int id)
        {
            var reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
                return false;
            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
