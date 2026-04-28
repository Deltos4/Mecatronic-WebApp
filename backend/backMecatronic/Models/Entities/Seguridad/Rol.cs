namespace backMecatronic.Models.Entities.Seguridad
{
    public class Rol
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; } = true;

        // Constructores
        public Rol() { } // Constructor para EF Cores
        public Rol(string nombre, string? descripcion = null)
        {
            NombreRol = nombre;
            DescripcionRol = descripcion;
        }

        // Navegación
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
