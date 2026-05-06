using backMecatronic.Models.DTOs.External;

namespace backMecatronic.Services.External
{
    public interface IApiPeruService
    {
        Task<ApiPeruDniResponseDto> ConsultarDni(string dni);
    }
}
