using backMecatronic.Models.DTOs.Inventario;

namespace backMecatronic.Services.Inventario
{
    public interface IProveedorService
    {
        Task<List<ProveedorDto>> ObtenerProveedores();
        Task<ProveedorResponseDto?> ObtenerProveedorPorId(int id);
        Task<ProveedorResponseDto> CrearProveedor(ProveedorCreateDto dto);
        Task<bool> ActualizarProveedor(int id, ProveedorUpdateDto dto);
        Task<bool> EliminarProveedor(int id);
    }
}
