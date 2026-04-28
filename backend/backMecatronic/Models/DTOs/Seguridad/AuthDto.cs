namespace backMecatronic.Models.DTOs.Seguridad
{
    public class RegisterDto
    {
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string CorreoUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }

    public class LoginDto
    {
        public string CorreoUsuario { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
    }

    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
