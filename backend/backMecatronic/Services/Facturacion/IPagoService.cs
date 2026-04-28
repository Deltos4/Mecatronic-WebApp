using backMecatronic.Models.DTOs.Facturacion;

namespace backMecatronic.Services.Facturacion
{
    public interface IPagoService
    {
        Task<List<PagoResponseDto>> ObtenerPagos();
        Task<PagoResponseDto> ObtenerPagoPorId(int id);
        Task<PagoResponseDto> RegistrarPago(PagoCreateDto dto);
        Task<bool> ActualizarPago(int id, PagoUpdateDto dto);
        Task<bool> EliminarPago(int id);

        // Métodos para métodos de pago
        Task<List<MetodoPagoResponseDto>> ObtenerMetodosPago();
    }
}
