using backMecatronic.Models.Entities.Maestros;

namespace backMecatronic.Models.Entities.Clientes
{
    public class ModeloVehiculo
    {
        public int IdModeloVehiculo { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdMarcaVehiculo { get; set; }
        public string NombreModeloVehiculo { get; set; } = null!;
        public string? DescripcionModeloVehiculo { get; set; }

        // Constructores
        public ModeloVehiculo() { } // Constructor para EF Core
        public ModeloVehiculo(int idTipo, int idMarca, string nombre)
        {
            IdTipoVehiculo = idTipo;
            IdMarcaVehiculo = idMarca;
            NombreModeloVehiculo = nombre;
        }

        // Navegación
        public MarcaVehiculo MarcaVehiculo { get; set; } = null!;
        public TipoVehiculo TipoVehiculo { get; set; } = null!;    
        public ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
    }
}
