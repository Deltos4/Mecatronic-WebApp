using backMecatronic.Models.DTOs.Operaciones;

namespace backMecatronic.Services.Operaciones
{
    public interface IOrdenServicioService
    {
        Task<List<OrdenServicioResponseDto>> ObtenerOrdenesServicio();
        Task<OrdenServicioResponseDto?> ObtenerOrdenServicioPorId(int id);
        Task<OrdenServicioResponseDto> CrearOrdenServicio(OrdenServicioCreateDto dto);
        Task<bool> ActualizarOrdenServicio(int id, OrdenServicioUpdateDto dto);
        Task<bool> EliminarOrdenServicio(int id);

        // Detalles
        Task<bool> AgregarDetalleOrden(int idOrden, DetalleOrdenCreateDto dto);
        
        // Que funcione con pedido y/o reserva
         Task<OrdenServicioResponseDto> CrearOrdenDesdePedido(int idPedido);
         Task<OrdenServicioResponseDto> CrearOrdenDesdeReserva(int idReserva);

        // Que funcione con proforma
        Task<OrdenServicioResponseDto> CrearOrdenDesdeProforma(int idProforma);
    }
}
