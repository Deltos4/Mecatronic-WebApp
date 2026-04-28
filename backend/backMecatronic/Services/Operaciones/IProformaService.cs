using backMecatronic.Models.DTOs.Operaciones;

namespace backMecatronic.Services.Operaciones
{
    public interface IProformaService
    {
        Task<List<ProformaResponseDto>> ObtenerProformas();
        Task<ProformaResponseDto?> ObtenerProformaPorId(int id);
        Task<ProformaResponseDto> CrearProforma(ProformaCreateDto dto);
        Task<bool> ActualizarProforma(int id, ProformaUpdateDto dto);
        Task<bool> EliminarProforma(int id);
    }
}
