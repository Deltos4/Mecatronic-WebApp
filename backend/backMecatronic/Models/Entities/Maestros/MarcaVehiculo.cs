using backMecatronic.Models.Entities.Clientes;

namespace backMecatronic.Models.Entities.Maestros
{
    public class MarcaVehiculo
    {
        public int IdMarcaVehiculo { get; set; }
        public string NombreMarcaVehiculo { get; set; } = null!;
        public string? DescripcionMarcaVehiculo { get; set; }

        // Constructores
        public MarcaVehiculo() { } // Constructor para EF Core
        public MarcaVehiculo(string nombre, string? descripcion = null)
        {
            NombreMarcaVehiculo = nombre;
            DescripcionMarcaVehiculo = descripcion;
        }

        // Navegación
        public ICollection<ModeloVehiculo> Modelos { get; set; } = new List<ModeloVehiculo>();
    }
}
