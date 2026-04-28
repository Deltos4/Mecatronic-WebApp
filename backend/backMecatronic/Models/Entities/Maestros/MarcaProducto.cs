using backMecatronic.Models.Entities.Inventario;

namespace backMecatronic.Models.Entities.Maestros
{
    public class MarcaProducto
    {
        public int IdMarcaProducto { get; set; }
        public string NombreMarcaProducto { get; set; } = null!;
        public string? DescripcionMarcaProducto { get; set; }
        public bool EstadoMarcaProducto { get; set; } = true;

        // Constructores
        public MarcaProducto() { } // Constructor para EF Core
        public MarcaProducto(string nombre, string? descripcion)
        {
            NombreMarcaProducto = nombre;
            DescripcionMarcaProducto = descripcion;
        }

        // Navegación
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
