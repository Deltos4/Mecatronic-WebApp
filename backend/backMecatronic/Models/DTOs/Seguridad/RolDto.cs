namespace backMecatronic.Models.DTOs.Seguridad
{
    public class RolDto
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; }
    }

    public class RolCreateDto
    {
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
    }

    public class RolUpdateDto
    {
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; }
    }

    public class RolResponseDto
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public string? DescripcionRol { get; set; }
        public bool EstadoRol { get; set; }
    }
}
