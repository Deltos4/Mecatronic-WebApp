using backMecatronic.Models.DTOs.Operaciones;

namespace backMecatronic.Services.Operaciones
{
    public interface IReservaService
    {
        Task<List<ReservaResponseDto>> ObtenerReservas();
        Task<ReservaResponseDto?> ObtenerReservaPorId(int id);
        Task<ReservaResponseDto> CrearReserva(ReservaCreateDto dto);
        Task<bool> ActualizarReserva(int id, ReservaUpdateDto dto);
        Task<bool> EliminarReserva(int id);
    }
}
