using backMecatronic.Models.DTOs.Notificaciones;

namespace backMecatronic.Services.Notificaciones
{
    public interface INotificacionService
    {
        Task<List<NotificacionDto>> GetByUsuarioAsync(int idUsuario);
        Task<int> CreateAsync(NotificacionCreateDto dto);
        Task<bool> MarcarLeidoAsync(int id);
        Task<int> ContarNoLeidasAsync(int idUsuario);
    }
}
