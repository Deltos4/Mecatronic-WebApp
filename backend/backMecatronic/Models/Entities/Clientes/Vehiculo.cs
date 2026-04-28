using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Clientes
{
    public class Vehiculo
    {
        public int IdVehiculo { get; set; }
        public int IdModeloVehiculo { get; set; }
        public string PlacaVehiculo { get; set; } = null!;
        public int? AnioVehiculo { get; set; }
        public string? ColorVehiculo { get; set; }
        public string? urlFotoVehiculo { get; set; }
        public bool EstadoVehiculo { get; set; } = true;

        // Constructores
        public Vehiculo() { } // Constructor para EF Core
        public Vehiculo(int idModelo, string placa) 
        {
            IdModeloVehiculo = idModelo;
            PlacaVehiculo = placa;
        }

        // Navegación
        public ModeloVehiculo ModeloVehiculo { get; set; } = null!;
        public ICollection<DetalleVehiculoCliente> DetalleClientes { get; set; } = new List<DetalleVehiculoCliente>();
        public ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
    }
}
