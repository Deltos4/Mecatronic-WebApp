using backMecatronic.Models.Entities.Personal;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class DetalleReservaServicio
    {
        public int IdDetalleRS { get; set; }
        public int IdReserva { get; set; }
        public int IdServicio { get; set; }       
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string? Observaciones { get; set; }

        // Constructores
        public DetalleReservaServicio() { } // Constructor para EF Core
        public DetalleReservaServicio(int idReserva, int idServicio, int cantidad, decimal precioUnitario, string? observaciones = null)
        {
            IdReserva = idReserva;
            IdServicio = idServicio;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = cantidad * precioUnitario;
            Observaciones = observaciones;
        }

        // Navegación
        public Reserva Reserva { get; set; } = null!;
        public Servicio Servicio { get; set; } = null!;
    }
}
