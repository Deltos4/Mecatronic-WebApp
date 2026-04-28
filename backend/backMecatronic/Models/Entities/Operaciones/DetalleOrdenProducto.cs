using backMecatronic.Models.Entities.Inventario;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class DetalleOrdenProducto
    {
        public int IdDetalleOP { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdProducto { get; set; }   
        public string? Descripcion { get; set; } 
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Constructores
        public DetalleOrdenProducto() { } // Constructor para EF Core
        public DetalleOrdenProducto(int idOrdenServicio, int idProducto, string descripcion, decimal cantidad, decimal precioUnitario)
        {
            IdOrdenServicio = idOrdenServicio;
            IdProducto = idProducto;
            Descripcion = descripcion;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
        }

        // Navegación
        public OrdenServicio OrdenServicio { get; set; } = null!;
        public Producto Producto { get; set; } = null!;

    }
}
