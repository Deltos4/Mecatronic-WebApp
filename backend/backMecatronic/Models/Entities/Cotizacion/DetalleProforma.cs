using backMecatronic.Models.Entities.Inventario;
using backMecatronic.Models.Entities.Personal;

namespace backMecatronic.Models.Entities.Cotizacion
{
    public class DetalleProforma
    {
        public int IdDetalleProforma { get; set; }
        public int IdProforma { get; set; }
        public int? IdProducto { get; set; }
        public int? IdServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
        // Constructores
        public DetalleProforma() { } // Constructor para EF Core
        public DetalleProforma(int idProforma, int? idProducto, int? idServicio, int cantidad, decimal precioUnitario, decimal subTotal)
        {
            IdProforma = idProforma;
            IdProducto = idProducto;
            IdServicio = idServicio;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            SubTotal = cantidad * precioUnitario;
        }

        // Navegación
        public Proforma Proforma { get; set; } = null!;
        public Producto? Producto { get; set; }
        public Servicio? Servicio { get; set; }
    }
}
