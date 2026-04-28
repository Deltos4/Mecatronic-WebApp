using backMecatronic.Models.DTOs.Maestros;

namespace backMecatronic.Services.Maestros
{
    public interface IMaestrosService
    {
        // Tipo de documento
        Task<List<TipoDocumentoDto>> ObtenerTiposDocumento();
        Task<TipoDocumentoResponseDto?> ObtenerTipoDocumentoPorId(int id);
        Task<TipoDocumentoResponseDto> CrearTipoDocumento(TipoDocumentoCreateDto dto);
        Task<bool> ActualizarTipoDocumento(int id, TipoDocumentoUpdateDto dto);
        Task<bool> EliminarTipoDocumento(int id);

        // Tipo de comprobante
        Task<List<TipoComprobanteDto>> ObtenerTiposComprobante();
        Task<TipoComprobanteResponseDto?> ObtenerTipoComprobantePorId(int id);
        Task<TipoComprobanteResponseDto> CrearTipoComprobante(TipoComprobanteCreateDto dto);
        Task<bool> ActualizarTipoComprobante(int id, TipoComprobanteUpdateDto dto);
        Task<bool> EliminarTipoComprobante(int id);
    }
}
