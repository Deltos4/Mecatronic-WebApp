using backMecatronic.Models.Entities.Cotizacion;
using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Personal
{
    public class Servicio
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; } = 0;
        public int DuracionServicio { get; set; } = 60; // Duración en minutos
        public bool EstadoServicio { get; set; } = true;
        public string? ImagenUrl { get; set; }

        // Constructores
        public Servicio() { } // Constructor para EF Core
        public Servicio(string nombre, decimal precio, int duracion)
        {
            NombreServicio = nombre;
            PrecioServicio = precio;
            DuracionServicio = duracion;
        }

        // Navegación
        public ICollection<DetalleProforma> Detalles { get; set; } = new List<DetalleProforma>();
        public ICollection<DetalleReservaServicio> DetalleReservas { get; set; } = new List<DetalleReservaServicio>();
    }
}
