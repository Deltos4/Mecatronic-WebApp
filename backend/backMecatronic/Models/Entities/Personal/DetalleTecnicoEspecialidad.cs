namespace backMecatronic.Models.Entities.Personal
{
    public class DetalleTecnicoEspecialidad
    {
        public int IdDetalleTE { get; set; }
        public int IdTecnico { get; set; }
        public int IdEspecialidad { get; set; }

        // Constructores
        public DetalleTecnicoEspecialidad() { } // Constructor para EF Core
        public DetalleTecnicoEspecialidad(int idTecnico, int idEspecialidad)
        {
            IdTecnico = idTecnico;
            IdEspecialidad = idEspecialidad;
        }

        // Navegación
        public Tecnico Tecnico { get; set; } = null!;
        public Especialidad Especialidad { get; set; } = null!;
    }
}
