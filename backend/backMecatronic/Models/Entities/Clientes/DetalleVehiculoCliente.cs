namespace backMecatronic.Models.Entities.Clientes
{
    public class DetalleVehiculoCliente
    {
        public int IdDetalleVC { get; set; }
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }       
        public DateTime? FechaRegistro { get; set; }
        public string? Observaciones { get; set; }

        // Constructores
        public DetalleVehiculoCliente() { } // Constructor para EF Core
        public DetalleVehiculoCliente(int idVehiculo, int idCliente)
        {
            IdVehiculo = idVehiculo;
            IdCliente = idCliente;
        }

        // Navegación
        public Vehiculo Vehiculo { get; set; } = null!;
        public Cliente Cliente { get; set; } = null!;
    }
}
