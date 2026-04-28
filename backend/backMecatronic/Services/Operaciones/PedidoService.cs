using backMecatronic.Data;
using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Models.Entities.Operaciones;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Operaciones
{
    public class PedidoService : IPedidoService
    {
        private readonly AppDbContext _context;

        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PedidoResponseDto>> ObtenerPedidos()
        {
            return await _context.Pedido
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Select(p => new PedidoResponseDto
                {
                    IdPedido = p.IdPedido,
                    NombreCliente = p.Cliente.NombreCliente,
                    FechaPedido = p.FechaPedido,
                    TotalPedido = p.TotalPedido,
                    EstadoPedido = p.EstadoPedido,
                    Detalles = p.Detalles.Select(d => new DetallePedidoResponseDto
                    {
                        NombreProducto = d.Producto.NombreProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        SubTotal = d.Subtotal
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<PedidoResponseDto?> ObtenerPedidoPorId(int id)
        {
            return await _context.Pedido
                .Where(p => p.IdPedido == id)
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .Select(p => new PedidoResponseDto
                {
                    IdPedido = p.IdPedido,
                    NombreCliente = p.Cliente.NombreCliente,
                    FechaPedido = p.FechaPedido,
                    TotalPedido = p.TotalPedido,
                    EstadoPedido = p.EstadoPedido,
                    Detalles = p.Detalles.Select(d => new DetallePedidoResponseDto
                    {
                        NombreProducto = d.Producto.NombreProducto,
                        Cantidad = d.Cantidad,
                        PrecioUnitario = d.PrecioUnitario,
                        SubTotal = d.Subtotal
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PedidoResponseDto> CrearPedido(PedidoCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(c => c.IdCliente == dto.IdCliente);

                if (cliente == null)
                    throw new Exception("Cliente no existe");

                decimal total = 0;

                var detalles = new List<DetallePedidoProducto>();

                foreach (var item in dto.Detalles)
                {
                    var producto = await _context.Producto
                        .FirstOrDefaultAsync(p => p.IdProducto == item.IdProducto);

                    if (producto == null)
                        throw new Exception($"Producto {item.IdProducto} no existe");

                    var subtotal = producto.PrecioProducto * item.Cantidad;
                    total += subtotal;

                    detalles.Add(new DetallePedidoProducto
                    {
                        IdProducto = producto.IdProducto,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = producto.PrecioProducto,
                        Subtotal = subtotal
                    });
                }

                var pedido = new Pedido
                {
                    IdCliente = dto.IdCliente,
                    FechaPedido = DateTime.Now,
                    TotalPedido = total,
                    EstadoPedido = "Pendiente",
                    Detalles = detalles
                };

                _context.Pedido.Add(pedido);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await ObtenerPedidoPorId(pedido.IdPedido)
                    ?? throw new Exception("Error al crear pedido");
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ActualizarPedido(int id, PedidoUpdateDto dto)
        {
            var pedido = await _context.Pedido.FindAsync(id);

            if (pedido == null) 
                return false;

            pedido.EstadoPedido = dto.EstadoPedido;
            pedido.ObservacionesPedido = dto.ObservacionesPedido;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarPedido(int id)
        {
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null) return false;
            _context.Pedido.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
