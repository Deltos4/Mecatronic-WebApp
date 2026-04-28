using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Notificaciones;

namespace backMecatronic.Models.Entities.Seguridad
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string? TelefonoUsuario { get; set; }
        public string? DireccionUsuario { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public bool EstadoUsuario { get; set; } = true;
        // Recuperación
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpiracion { get; set; }

        // Constructores
        public Usuario () { } // Constructor para EF Core
        public Usuario(int idRol, string nombre, string correo, string contrasena)
        {
            IdRol = idRol;
            NombreUsuario = nombre;
            CorreoUsuario = correo;
            ContrasenaUsuario = contrasena;
        }

        // Navegación
        public Rol Rol { get; set; } = null!;
        public Cliente? Cliente { get; set; }
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion> ();
    }
}
