using backMecatronic.Models.Entities.Maestros;

namespace backMecatronic.Models.Entities.Inventario
{
    public class Proveedor
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumentoProveedor { get; set; }
        public string? ContactoProveedor { get; set; }
        public string? TelefonoProveedor { get; set; }
        public string? CorreoProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
        public bool EstadoProveedor { get; set; } = true;

        // Constructores
        public Proveedor() { } // Constructor para EF Core
        public Proveedor(string nombre, int? idDocumento, string? numeroDocumento, string? contacto, string? telefono, string? correo, string? direccion)
        {
            NombreProveedor = nombre;
            IdTipoDocumento = idDocumento;
            NumeroDocumentoProveedor = numeroDocumento;
            ContactoProveedor = contacto;
            TelefonoProveedor = telefono;
            CorreoProveedor = correo;
            DireccionProveedor = direccion;
        }

        // Navegación
        public TipoDocumento? TipoDocumento { get; set; }
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
