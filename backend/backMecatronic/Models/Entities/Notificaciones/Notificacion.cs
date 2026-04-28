using backMecatronic.Models.Entities.Operaciones;
using backMecatronic.Models.Entities.Seguridad;

namespace backMecatronic.Models.Entities.Notificaciones
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdOrdenServicio { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public bool Leido { get; set; } = false;

        // Constructores
        public Notificacion() { } // Constructor para EF Core
        public Notificacion(int idUsuario, int? idReserva, int? idPedido, int? idOrdenServicio, string titulo, string mensaje)
        {
            IdUsuario = idUsuario;
            IdReserva = idReserva;
            IdPedido = idPedido;
            IdOrdenServicio = idOrdenServicio;
            Titulo = titulo;
            Mensaje = mensaje;
        }

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Reserva? Reserva { get; set; }
        public Pedido? Pedido { get; set; }
        public OrdenServicio? OrdenServicio { get; set; }
    }
}
