using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Notificaciones;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = null!; // Pendiente, Confirmada, Cancelada, Completada 

        // Constructores
        public Reserva() { } // Constructor para EF Core
        public Reserva(int idCliente, DateTime fechaProgramada)
        {
            IdCliente = idCliente;
            FechaProgramada = fechaProgramada;
            Estado = "Pendiente";
        }

        // Navegación
        public Cliente Cliente { get; set; } = null!;
        public OrdenServicio? OrdenServicio { get; set; }
        public ICollection<DetalleReservaServicio> Detalles { get; set; } = new List<DetalleReservaServicio>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}
