using backMecatronic.Models.Entities.Clientes;

namespace backMecatronic.Models.Entities.Maestros
{
    public class TipoVehiculo
    {
        public int IdTipoVehiculo { get; set; }
        public string NombreTipoVehiculo { get; set; } = null!;
        public string? DescripcionTipoVehiculo { get; set; }

        // Constructores
        public TipoVehiculo() { } // Constructor para EF Core
        public TipoVehiculo(string nombre, string? descripcion = null)
        {
            NombreTipoVehiculo = nombre;
            DescripcionTipoVehiculo = descripcion;
        }

        // Navegación
        public ICollection<ModeloVehiculo> Modelos { get; set; } = new List<ModeloVehiculo>();
    }
}
