using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Cotizacion
{
    public class Proforma
    {
        public int IdProforma { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;
        public DateTime FechaVencimiento { get; set; }
        public decimal Total { get; set; }

        // Constructores
        public Proforma() { } // Constructor para EF Core
        public Proforma(int idCliente, DateTime fechaVencimiento, decimal total)
        {
            IdCliente = idCliente;
            FechaVencimiento = fechaVencimiento;
            Total = total;
        }

        // Navegación
        public Cliente Cliente { get; set; } = null!;
        public List<DetalleProforma> Detalles { get; set; } = new List<DetalleProforma>();
        public List<OrdenServicio> OrdenesServicio { get; set; } = new List<OrdenServicio>();
    }
}
