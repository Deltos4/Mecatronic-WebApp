using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Facturacion
{
    public class Pago
    {
        public int IdPago { get; set; }
        public int IdOrdenServicio { get; set; }   
        public int IdMetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }

        // Constructores
        public Pago() { } // Constructor para EF Core
        public Pago(int idOrdenServicio, int idMetodoPago, DateTime fechaPago, decimal monto)
        {
            IdOrdenServicio = idOrdenServicio;
            IdMetodoPago = idMetodoPago;
            FechaPago = fechaPago;
            Monto = monto;
        }

        // Navegación   
        public MetodoPago MetodoPago { get; set; } = null!;
        public OrdenServicio OrdenServicio { get; set; } = null!;
    }
}
