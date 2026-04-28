using backMecatronic.Models.DTOs.Personal;

namespace backMecatronic.Services.Personal
{
    public interface IServicioService
    {
        Task<List<ServicioResponseDto>> ObtenerServicios();
        Task<ServicioResponseDto?> ObtenerServicioPorId(int id);
        Task<ServicioResponseDto> CrearServicio(ServicioCreateDto dto);
        Task<bool> ActualizarServicio(int id, ServicioUpdateDto dto);
        Task<bool> EliminarServicio(int id);
    }
}
