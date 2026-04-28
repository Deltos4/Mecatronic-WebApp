using backMecatronic.Models.Entities.Inventario;

namespace backMecatronic.Models.Entities.Maestros
{
    public class CategoriaProducto
    {
        public int IdCategoriaProducto { get; set; }
        public string NombreCategoriaProducto { get; set; } = null!;
        public string? DescripcionCategoriaProducto { get; set; }

        // Constructores
        public CategoriaProducto() { } // Constructor para EF Core
        public CategoriaProducto(string nombre, string? descripcion)
        {
            NombreCategoriaProducto = nombre;
            DescripcionCategoriaProducto = descripcion;
        }

        // Navegación
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
