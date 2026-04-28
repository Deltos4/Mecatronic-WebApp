using backMecatronic.Data;
using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Models.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Operaciones
{
    public class OrdenServicioService : IOrdenServicioService
    {
        private readonly AppDbContext _context;

        public OrdenServicioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrdenServicioResponseDto>> ObtenerOrdenesServicio()
        {
            var ordenes = await _context.OrdenServicio
                .Include(o => o.Cliente)
                .Include(o => o.Detalles)
                .Select(o => new OrdenServicioResponseDto
                {
                    IdOrdenServicio = o.IdOrdenServicio,
                    IdCliente = o.IdCliente,
                    NombreCliente = o.Cliente != null ? o.Cliente.NombreCliente : null,
                    IdReserva = o.IdReserva,
                    IdPedido = o.IdPedido,
                    IdProforma = o.IdProforma,
                    IdVehiculo = o.IdVehiculo,
                    IdTecnico = o.IdTecnico,
                    FechaInicio = o.FechaInicio,
                    FechaFin = o.FechaFin,
                    Total = o.Total,
                    TipoItem = (int)o.TipoItem,
                    DetalleOrden = o.Detalles.Select(d => new DetalleOrdenResponseDto
                    {
                        IdDetalleOP = d.IdDetalleOP,
                        IdProducto = d.IdProducto,
                        NombreProducto = d.Producto.NombreProducto,
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Cantidad * d.PrecioUnitario
                    }).ToList()
                })
                .ToListAsync();
            return ordenes;
        }

        public async Task<OrdenServicioResponseDto?> ObtenerOrdenServicioPorId(int id)
        {
            var orden = await _context.OrdenServicio
                .Include(o => o.Cliente)
                .Include(o => o.Detalles)
                .Where(o => o.IdOrdenServicio == id)
                .Select(o => new OrdenServicioResponseDto
                {
                    IdOrdenServicio = o.IdOrdenServicio,
                    IdCliente = o.IdCliente,
                    NombreCliente = o.Cliente != null ? o.Cliente.NombreCliente : null,
                    IdReserva = o.IdReserva,
                    IdPedido = o.IdPedido,
                    IdProforma = o.IdProforma,
                    IdVehiculo = o.IdVehiculo,
                    IdTecnico = o.IdTecnico,
                    FechaInicio = o.FechaInicio,
                    FechaFin = o.FechaFin,
                    Total = o.Total,
                    TipoItem = (int)o.TipoItem,
                    DetalleOrden = o.Detalles.Select(d => new DetalleOrdenResponseDto
                    {
                        IdDetalleOP = d.IdDetalleOP,
                        IdProducto = d.IdProducto,
                        NombreProducto = d.Producto.NombreProducto,
                        Descripcion = d.Descripcion,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        Subtotal = d.Cantidad * d.PrecioUnitario
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            return orden;
        }

        public async Task<OrdenServicioResponseDto> CrearOrdenServicio(OrdenServicioCreateDto dto)
        {
            var orden = new OrdenServicio
            {
                IdCliente = dto.IdCliente,
                IdReserva = dto.IdReserva,
                IdPedido = dto.IdPedido,
                IdProforma = dto.IdProforma,
                IdVehiculo = dto.IdVehiculo,
                IdTecnico = dto.IdTecnico,
                FechaInicio = dto.FechaInicio,
                Total = dto.Total,
                TipoItem = (TipoItemOrden)dto.TipoItem
            };
            _context.OrdenServicio.Add(orden);
            await _context.SaveChangesAsync();
            return new OrdenServicioResponseDto
            {
                IdOrdenServicio = orden.IdOrdenServicio,
                IdCliente = orden.IdCliente,
                IdReserva = orden.IdReserva,
                IdPedido = orden.IdPedido,
                IdProforma = orden.IdProforma,
                IdVehiculo = orden.IdVehiculo,
                IdTecnico = orden.IdTecnico,
                FechaInicio = orden.FechaInicio,
                Total = orden.Total,
                TipoItem = (int)orden.TipoItem,
                DetalleOrden = new List<DetalleOrdenResponseDto>()
            };
        }

        public async Task<bool> ActualizarOrdenServicio(int id, OrdenServicioUpdateDto dto)
        {
            var orden = await _context.OrdenServicio.FindAsync(id);

            if (orden == null)
                return false;

            if (dto.Estado != null)
                orden.Estado = dto.Estado;

            if (dto.FechaFin.HasValue)
                orden.FechaFin = dto.FechaFin.Value;

            _context.OrdenServicio.Update(orden);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarOrdenServicio(int id)
        {
            var orden = await _context.OrdenServicio.FindAsync(id);
            if (orden == null) return false;
            _context.OrdenServicio.Remove(orden);
            await _context.SaveChangesAsync();
            return true;
        }

        // Detalles
        public async Task<bool> AgregarDetalleOrden(int idOrden, DetalleOrdenCreateDto dto)
        {
            var orden = await _context.OrdenServicio.FindAsync(idOrden);
            if (orden == null) return false;
            var detalle = new DetalleOrdenDto
            {
                IdDetalleOP = idOrden,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            };

            _context.DetalleOrdenProducto.Add(new DetalleOrdenProducto
            {
                IdOrdenServicio = idOrden,
                IdProducto = dto.IdProducto,
                Cantidad = dto.Cantidad,
                PrecioUnitario = dto.PrecioUnitario
            });

            await _context.SaveChangesAsync();
            return true;
        }

        // Que funcione con pedido y/o reserva
        public async Task<OrdenServicioResponseDto> CrearOrdenDesdePedido(int idPedido)
        {
            var pedido = await _context.Pedido
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.IdPedido == idPedido);

            if (pedido == null) throw new Exception("Pedido no encontrado");
            var orden = new OrdenServicio
            {
                IdCliente = pedido.IdCliente,
                IdPedido = pedido.IdPedido,
                FechaInicio = DateTime.Now,
                Total = pedido.TotalPedido,
                TipoItem = TipoItemOrden.Producto
            };

            _context.OrdenServicio.Add(orden);
            await _context.SaveChangesAsync();

            return new OrdenServicioResponseDto
            {
                IdOrdenServicio = orden.IdOrdenServicio,
                IdCliente = orden.IdCliente,
                NombreCliente = pedido.Cliente != null ? pedido.Cliente.NombreCliente : null,
                IdPedido = orden.IdPedido,
                IdVehiculo = orden.IdVehiculo,
                FechaInicio = orden.FechaInicio,
                Total = orden.Total,
                TipoItem = (int)orden.TipoItem,
                DetalleOrden = new List<DetalleOrdenResponseDto>()
            };
        }

        public async Task<OrdenServicioResponseDto> CrearOrdenDesdeReserva(int idReserva)
        {
            var reserva = await _context.Reserva
                .Include(r => r.Cliente)
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null) throw new Exception("Reserva no encontrada");
            var orden = new OrdenServicio
            {
                IdCliente = reserva.IdCliente,
                IdReserva = reserva.IdReserva,
                FechaInicio = DateTime.Now,
                Total = 0, // Se calculará al agregar detalles
                TipoItem = TipoItemOrden.Servicio
            };

            _context.OrdenServicio.Add(orden);
            await _context.SaveChangesAsync();

            return new OrdenServicioResponseDto
            {
                IdOrdenServicio = orden.IdOrdenServicio,
                IdCliente = orden.IdCliente,
                NombreCliente = reserva.Cliente != null ? reserva.Cliente.NombreCliente : null,
                IdReserva = orden.IdReserva,
                IdVehiculo = orden.IdVehiculo,
                FechaInicio = orden.FechaInicio,
                Total = orden.Total,
                TipoItem = (int)orden.TipoItem,
                DetalleOrden = new List<DetalleOrdenResponseDto>()
            };
        }

        // Que funcione con proforma
        public async Task<OrdenServicioResponseDto> CrearOrdenDesdeProforma(int idProforma)
        {
            var proforma = await _context.Proforma
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(p => p.IdProforma == idProforma);

            if (proforma == null) throw new Exception("Proforma no encontrada");
            var orden = new OrdenServicio
            {
                IdCliente = proforma.IdCliente,
                IdProforma = proforma.IdProforma,
                FechaInicio = DateTime.Now,
                Total = proforma.Total,
                TipoItem = TipoItemOrden.Servicio
            };

            _context.OrdenServicio.Add(orden);
            await _context.SaveChangesAsync();

            return new OrdenServicioResponseDto
            {
                IdOrdenServicio = orden.IdOrdenServicio,
                IdCliente = orden.IdCliente,
                NombreCliente = proforma.Cliente != null ? proforma.Cliente.NombreCliente : null,
                IdProforma = orden.IdProforma,
                IdVehiculo = orden.IdVehiculo,
                FechaInicio = orden.FechaInicio,
                Total = orden.Total,
                TipoItem = (int)orden.TipoItem,
                DetalleOrden = new List<DetalleOrdenResponseDto>()
            };
        }
    }
}
