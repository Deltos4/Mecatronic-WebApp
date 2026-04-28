using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Models.DTOs.Seguridad;

namespace backMecatronic.Services.Seguridad
{
    public interface IRolService
    {
        Task<List<RolResponseDto>> ObtenerRoles();
        Task<RolResponseDto?> ObtenerRolPorId(int id);
        Task<RolResponseDto> CrearRol(RolCreateDto dto);
        Task<bool> ActualizarRol(int id, RolUpdateDto dto);
        Task<bool> EliminarRol(int id);
    }
}
