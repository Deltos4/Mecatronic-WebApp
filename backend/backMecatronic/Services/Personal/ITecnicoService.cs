using backMecatronic.Models.DTOs.Personal;

namespace backMecatronic.Services.Personal
{
    public interface ITecnicoService
    {
        Task<List<TecnicoResponseDto>> ObtenerTecnicos();
        Task<TecnicoResponseDto?> ObtenerTecnicoPorId(int id);
        Task<TecnicoResponseDto> CrearTecnico(TecnicoCreateDto dto);
        Task<bool> ActualizarTecnico(int id, TecnicoUpdateDto dto);
        Task<bool> EliminarTecnico(int id);

        // Asignación
        Task<bool> AsignarEspecialidadTecnico(DetalleTecnicoEspecialidadDto dto);

        // Catálogos
        Task<List<EspecialidadDto>> ObtenerEspecialidades();
    }
}
