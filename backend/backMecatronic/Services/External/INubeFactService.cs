using backMecatronic.Models.DTOs.External;

namespace backMecatronic.Services.External
{
    public interface INubeFactService
    {
        Task<NubeFactResponseDto> Enviar(NubeFactRequestDto request);
    }
}
