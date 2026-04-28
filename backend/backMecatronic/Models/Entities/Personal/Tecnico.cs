using backMecatronic.Models.Entities.Operaciones;

namespace backMecatronic.Models.Entities.Personal
{
    public class Tecnico
    {
        public int IdTecnico { get; set; }
        public string DniEmpleado { get; set; } = null!;
        public string CargoEmpleado { get; set; } = null!;
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
        public bool EstadoEmpleado { get; set; } = true;

        // Constructores
        public Tecnico () { } // Constructor para EF Core
        public Tecnico(string dni, string cargo, string nombre, string apellido, DateTime fechaContratacion)
        {
            DniEmpleado = dni;
            CargoEmpleado = cargo;
            NombreEmpleado = nombre;
            ApellidoEmpleado = apellido;
            FechaContratacion = fechaContratacion;
        }

        // Navegación
        public ICollection<DetalleTecnicoEspecialidad> DetallesTecnicoEspecialidad { get; set; } = new List<DetalleTecnicoEspecialidad>();
        public ICollection<OrdenServicio> OrdenServicios { get; set; } = new List<OrdenServicio>();
    }
}
