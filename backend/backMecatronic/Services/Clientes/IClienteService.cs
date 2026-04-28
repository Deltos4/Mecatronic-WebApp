using backMecatronic.Models.DTOs.Clientes;

namespace backMecatronic.Services.Clientes
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> ObtenerClientes();
        Task<ClienteResponseDto?> ObtenerClientePorId(int id);
        Task<ClienteResponseDto> CrearCliente(ClienteCreateDto dto);
        Task<bool> ActualizarCliente(int id, ClienteUpdateDto dto);
        Task<bool> EliminarCliente(int id);
    }
}
