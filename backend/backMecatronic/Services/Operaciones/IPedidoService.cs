using backMecatronic.Models.DTOs.Operaciones;

namespace backMecatronic.Services.Operaciones
{
    public interface IPedidoService
    {
        Task<List<PedidoResponseDto>> ObtenerPedidos();
        Task<PedidoResponseDto?> ObtenerPedidoPorId(int id);
        Task<PedidoResponseDto> CrearPedido(PedidoCreateDto dto);
        Task<bool> ActualizarPedido(int id, PedidoUpdateDto dto);
        Task<bool> EliminarPedido(int id);
    }
}
