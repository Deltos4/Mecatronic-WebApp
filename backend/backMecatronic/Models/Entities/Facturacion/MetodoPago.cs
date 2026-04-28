using Microsoft.AspNetCore.Mvc.RazorPages;

namespace backMecatronic.Models.Entities.Facturacion
{
    public class MetodoPago
    {
        public int IdMetodoPago { get; set; }
        public string NombreMetodoPago { get; set; } = null!; // Efectivo, Tarjeta, Transferencia, Yape, Plin, etc.
        public string? DescripcionMetodoPago { get; set; }
        public bool EstadoMetodoPago { get; set; } = true;

        // Constructores
        public MetodoPago() { } // Constructor para EF Core
        public MetodoPago(string nombre, string? descripcion = null)
        {
            NombreMetodoPago = nombre;
            DescripcionMetodoPago = descripcion;
        }

        // Navegación
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }
}
