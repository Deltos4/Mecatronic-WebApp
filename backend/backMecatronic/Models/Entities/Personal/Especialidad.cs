namespace backMecatronic.Models.Entities.Personal
{
    public class Especialidad
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; } = null!;
        public string? DescripcionEspecialidad { get; set; }

        // Constructores
        public Especialidad() { } // Constructor para EF Core
        public Especialidad(string nombre, string? descripcion = null)
        {
            NombreEspecialidad = nombre;
            DescripcionEspecialidad = descripcion;
        }

        // Navegación
        public ICollection<DetalleTecnicoEspecialidad> DetallesTecnicoEspecialidad { get; set; } = new List<DetalleTecnicoEspecialidad>();
    }
}
