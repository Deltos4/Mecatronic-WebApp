using backMecatronic.Models.DTOs.Seguridad;

namespace backMecatronic.Services.Seguridad
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponseDto>> ObtenerUsuarios();
        Task<UsuarioResponseDto?> ObtenerUsuarioPorId(int id);
        Task<UsuarioResponseDto> CrearUsuario(UsuarioCreateDto dto);
        Task<bool> ActualizarUsuario(int id, UsuarioUpdateDto dto);
        Task<bool> EliminarUsuario(int id);
    }
}
