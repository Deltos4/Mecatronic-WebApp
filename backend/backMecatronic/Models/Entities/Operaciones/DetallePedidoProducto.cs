using backMecatronic.Models.Entities.Inventario;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class DetallePedidoProducto
    {
        public int IdDetallePP { get; set; }
        public int IdPedido { get; set; }      
        public int IdProducto { get; set; }        
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Constructores
        public DetallePedidoProducto() { } // Constructor para EF Core
        public DetallePedidoProducto(int idPedido, int idProducto, int cantidad, decimal precioUnitario)
        {
            IdPedido = idPedido;
            IdProducto = idProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
        }

        // Navegación
        public Pedido Pedido { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}
