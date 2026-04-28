using backMecatronic.Models.Entities.Maestros;
using backMecatronic.Models.Entities.Operaciones;
using backMecatronic.Models.Entities.Seguridad;
using backMecatronic.Models.Entities.Cotizacion;

namespace backMecatronic.Models.Entities.Clientes
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public int? IdUsuario { get; set; }      
        public int IdTipoDocumento { get; set; } = 1; // Por defecto DNI       
        public string NumeroDocumentoCliente { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public bool EstadoCliente { get; set; } = true;

        // Constructores
        public Cliente() { } // Constructor para EF Core
        public Cliente(int idDocumento, string numeroDocumento, string nombre, string apellido) 
        {
            IdTipoDocumento = idDocumento;
            NumeroDocumentoCliente = numeroDocumento;
            NombreCliente = nombre;
            ApellidoCliente = apellido;
        }

        // Navegación
        public Usuario? Usuario { get; set; }
        public TipoDocumento TipoDocumento { get; set; } = null!;
        public ICollection<DetalleVehiculoCliente> DetalleVehiculos { get; set; } = new List<DetalleVehiculoCliente>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ICollection<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
        public ICollection<Proforma> Proformas { get; set; } = new List<Proforma>();
    }
}
