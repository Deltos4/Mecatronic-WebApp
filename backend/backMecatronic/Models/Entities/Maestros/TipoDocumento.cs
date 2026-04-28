using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Inventario;

namespace backMecatronic.Models.Entities.Maestros
{
    public class TipoDocumento
    {
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; } = null!;
        public string? DescripcionTipoDocumento { get; set; }

        // Constructores
        public TipoDocumento() { } // Constructor para EF Core
        public TipoDocumento(string nombre, string? descripcion = null)
        {
            NombreTipoDocumento = nombre;
            DescripcionTipoDocumento = descripcion;
        }

        // Navegación
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public ICollection<Proveedor> Proveedores { get; set; } = new List<Proveedor>();
    }
}
