using backMecatronic.Models.DTOs.Seguridad;

public interface IAuthService
{
    Task<UsuarioResponseDto> Registrar(RegisterDto dto);
    Task<AuthResponseDto> Login(LoginDto dto);
}
