using backMecatronic.Models.Entities.Cotizacion;
using backMecatronic.Models.Entities.Maestros;
using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Inventario
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public int IdMarcaProducto { get; set; }        
        public int IdCategoriaProducto { get; set; }  
        public int? IdProveedor { get; set; }      
        public string NombreProducto { get; set; } = null!;
        public string? DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? ImagenUrl { get; set; }
        public bool EstadoProducto { get; set; } = true;

        // Constructores
        public Producto() { } // Constructor para EF Core
        public Producto(int idMarca, int idCategoria, int? idProveedor, string nombre, string? descripcion, decimal precio, string? imagenUrl)
        {
            IdMarcaProducto = idMarca;
            IdCategoriaProducto = idCategoria;
            IdProveedor = idProveedor;
            NombreProducto = nombre;
            DescripcionProducto = descripcion;
            PrecioProducto = precio;
            ImagenUrl = imagenUrl;
        }

        // Navegación
        public MarcaProducto MarcaProducto { get; set; } = null!;
        public CategoriaProducto CategoriaProducto { get; set; } = null!;
        public Proveedor? Proveedor { get; set; }
        public Stock Stock { get; set; } = null!;
        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
        public ICollection<DetalleProforma> Detalles { get; set; } = new List<DetalleProforma>();
        public ICollection<DetallePedidoProducto> DetallesPedido { get; set; } = new List<DetallePedidoProducto>();
        public ICollection<DetalleOrdenProducto> DetallesOrdenServicio { get; set; } = new List<DetalleOrdenProducto>();
    }
}
