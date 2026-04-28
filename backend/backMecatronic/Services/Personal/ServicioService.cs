using backMecatronic.Data;
using backMecatronic.Models.DTOs.Personal;
using backMecatronic.Models.Entities.Personal;
using backMecatronic.Services.Personal;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Personal
{
    public class ServicioService : IServicioService
    {
        private readonly AppDbContext _context;

        public ServicioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServicioResponseDto>> ObtenerServicios()
        {
            return await _context.Servicio
                .Select(s => new ServicioResponseDto
                {
                    IdServicio = s.IdServicio,
                    NombreServicio = s.NombreServicio,
                    DescripcionServicio = s.DescripcionServicio,
                    PrecioServicio = s.PrecioServicio,
                    DuracionServicio = s.DuracionServicio,
                    EstadoServicio = s.EstadoServicio,
                    ImagenUrl = s.ImagenUrl
                })
                .ToListAsync();
        }

        public async Task<ServicioResponseDto?> ObtenerServicioPorId(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null) return null;

            return new ServicioResponseDto
            {
                IdServicio = servicio.IdServicio,
                NombreServicio = servicio.NombreServicio,
                DescripcionServicio = servicio.DescripcionServicio,
                PrecioServicio = servicio.PrecioServicio,
                DuracionServicio = servicio.DuracionServicio,
                EstadoServicio = servicio.EstadoServicio,
                ImagenUrl = servicio.ImagenUrl
            };
        }

        public async Task<ServicioResponseDto> CrearServicio(ServicioCreateDto dto)
        {
            var servicio = new Servicio
            {
                NombreServicio = dto.NombreServicio,
                DescripcionServicio = dto.DescripcionServicio,
                PrecioServicio = dto.PrecioServicio,
                DuracionServicio = dto.DuracionServicio,
                ImagenUrl = dto.ImagenUrl,
                EstadoServicio = true
            };

            _context.Servicio.Add(servicio);
            await _context.SaveChangesAsync();

            return new ServicioResponseDto
            {
                IdServicio = servicio.IdServicio,
                NombreServicio = servicio.NombreServicio,
                DescripcionServicio = servicio.DescripcionServicio,
                PrecioServicio = servicio.PrecioServicio,
                DuracionServicio = servicio.DuracionServicio,
                EstadoServicio = servicio.EstadoServicio,
                ImagenUrl = servicio.ImagenUrl
            };
        }

        public async Task<bool> ActualizarServicio(int id, ServicioUpdateDto dto)
        {
            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null) return false;

            servicio.NombreServicio = dto.NombreServicio;
            servicio.DescripcionServicio = dto.DescripcionServicio;
            servicio.PrecioServicio = dto.PrecioServicio;
            servicio.DuracionServicio = dto.DuracionServicio;
            servicio.EstadoServicio = dto.EstadoServicio;
            servicio.ImagenUrl = dto.ImagenUrl;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarServicio(int id)
        {
            var servicio = await _context.Servicio.FindAsync(id);

            if (servicio == null) return false;

            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
