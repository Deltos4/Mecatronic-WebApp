using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Notificaciones;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal TotalPedido { get; set; }
        public string EstadoPedido { get; set; } = null!; // Pendiente, Procesando, Completado, Cancelado
        public string? ObservacionesPedido { get; set; }

        // Constructores
        public Pedido() { } // Constructor para EF Core
        public Pedido(int idCliente, DateTime fechaPedido, decimal totalPedido, string estadoPedido, string? observaciones)
        {
            IdCliente = idCliente;
            FechaPedido = fechaPedido;
            TotalPedido = totalPedido;
            EstadoPedido = estadoPedido;
            ObservacionesPedido = observaciones;
        }

        // Navegación
        public Cliente Cliente { get; set; } = null!;
        public OrdenServicio? OrdenServicio { get; set; }
        public ICollection<DetallePedidoProducto> Detalles { get; set; } = new List<DetallePedidoProducto>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}
