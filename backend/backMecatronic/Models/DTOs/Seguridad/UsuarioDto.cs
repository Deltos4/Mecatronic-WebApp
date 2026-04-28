namespace backMecatronic.Models.DTOs.Seguridad
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string? TelefonoUsuario { get; set; }
        public string? DireccionUsuario { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public bool EstadoUsuario { get; set; } = true;
    }

    public class UsuarioCreateDto
    {
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string? TelefonoUsuario { get; set; }
        public string? DireccionUsuario { get; set; }
    }

    public class UsuarioUpdateDto
    {
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string ContrasenaUsuario { get; set; } = null!;
        public string? TelefonoUsuario { get; set; }
        public string? DireccionUsuario { get; set; }
        public bool EstadoUsuario { get; set; } = true;
    }

    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string Rol { get; set; } = null!;
        public bool EstadoUsuario { get; set; }
    }
}
