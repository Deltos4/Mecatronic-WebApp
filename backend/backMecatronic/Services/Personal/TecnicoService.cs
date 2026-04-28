using Microsoft.EntityFrameworkCore;
using backMecatronic.Data;
using backMecatronic.Models.DTOs.Personal;
using backMecatronic.Models.Entities.Personal;

namespace backMecatronic.Services.Personal
{
    public class TecnicoService : ITecnicoService
    {
        private readonly AppDbContext _context;

        public TecnicoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TecnicoResponseDto>> ObtenerTecnicos()
        {
            return await _context.Tecnico
                .Include(t => t.DetallesTecnicoEspecialidad)
                    .ThenInclude(te => te.Especialidad)
                .Select(t => new TecnicoResponseDto
                {
                    IdTecnico = t.IdTecnico,
                    DniEmpleado = t.DniEmpleado,
                    CargoEmpleado = t.CargoEmpleado,
                    NombreEmpleado = t.NombreEmpleado,
                    ApellidoEmpleado = t.ApellidoEmpleado,
                    TelefonoEmpleado = t.TelefonoEmpleado,
                    CorreoEmpleado = t.CorreoEmpleado,
                    DireccionEmpleado = t.DireccionEmpleado,
                    FechaContratacion = t.FechaContratacion,
                    EstadoEmpleado = t.EstadoEmpleado,
                    Especialidades = t.DetallesTecnicoEspecialidad.Select(te => new EspecialidadDto
                    {
                        IdEspecialidad = te.Especialidad.IdEspecialidad,
                        NombreEspecialidad = te.Especialidad.NombreEspecialidad
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<TecnicoResponseDto?> ObtenerTecnicoPorId(int id)
        {
            return await _context.Tecnico
                .Include(t => t.DetallesTecnicoEspecialidad)
                    .ThenInclude(te => te.Especialidad)
                .Where(t => t.IdTecnico == id)
                .Select(t => new TecnicoResponseDto
                {
                    IdTecnico = t.IdTecnico,
                    DniEmpleado = t.DniEmpleado,
                    CargoEmpleado = t.CargoEmpleado,
                    NombreEmpleado = t.NombreEmpleado,
                    ApellidoEmpleado = t.ApellidoEmpleado,
                    TelefonoEmpleado = t.TelefonoEmpleado,
                    CorreoEmpleado = t.CorreoEmpleado,
                    DireccionEmpleado = t.DireccionEmpleado,
                    FechaContratacion = t.FechaContratacion,
                    EstadoEmpleado = t.EstadoEmpleado,
                    Especialidades = t.DetallesTecnicoEspecialidad.Select(te => new EspecialidadDto
                    {
                        IdEspecialidad = te.Especialidad.IdEspecialidad,
                        NombreEspecialidad = te.Especialidad.NombreEspecialidad
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<TecnicoResponseDto> CrearTecnico(TecnicoCreateDto dto)
        {
            var tecnico = new Tecnico
            {
                DniEmpleado = dto.DniEmpleado,
                CargoEmpleado = dto.CargoEmpleado,
                NombreEmpleado = dto.NombreEmpleado,
                ApellidoEmpleado = dto.ApellidoEmpleado,
                TelefonoEmpleado = dto.TelefonoEmpleado,
                CorreoEmpleado = dto.CorreoEmpleado,
                DireccionEmpleado = dto.DireccionEmpleado,
                FechaContratacion = dto.FechaContratacion,
                EstadoEmpleado = true
            };

            _context.Tecnico.Add(tecnico);
            await _context.SaveChangesAsync();

            return await ObtenerTecnicoPorId(tecnico.IdTecnico)
                ?? throw new Exception("Error al crear el técnico");
        }

        public async Task<bool> ActualizarTecnico(int id, TecnicoUpdateDto dto)
        {
            var tecnico = await _context.Tecnico.FindAsync(id);

            if (tecnico == null) return false;

            tecnico.DniEmpleado = dto.DniEmpleado;
            tecnico.CargoEmpleado = dto.CargoEmpleado;
            tecnico.NombreEmpleado = dto.NombreEmpleado;
            tecnico.ApellidoEmpleado = dto.ApellidoEmpleado;
            tecnico.TelefonoEmpleado = dto.TelefonoEmpleado;
            tecnico.CorreoEmpleado = dto.CorreoEmpleado;
            tecnico.DireccionEmpleado = dto.DireccionEmpleado;
            tecnico.FechaContratacion = dto.FechaContratacion;
            tecnico.EstadoEmpleado = dto.EstadoEmpleado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTecnico(int id)
        {
            var tecnico = await _context.Tecnico.FindAsync(id);

            if (tecnico == null) return false;

            _context.Tecnico.Remove(tecnico);
            await _context.SaveChangesAsync();

            return true;
        }

        // Asignación
        public async Task<bool> AsignarEspecialidadTecnico(DetalleTecnicoEspecialidadDto dto)
        {
            var existe = await _context.DetalleTecnicoEspecialidad
                .AnyAsync(d => d.IdTecnico == dto.IdTecnico && d.IdEspecialidad == dto.IdEspecialidad);

            if (existe) return true;

            var detalle = new DetalleTecnicoEspecialidad
            {
                IdTecnico = dto.IdTecnico,
                IdEspecialidad = dto.IdEspecialidad
            };

            _context.DetalleTecnicoEspecialidad.Add(detalle);
            await _context.SaveChangesAsync();

            return true;
        }

        // Catálogos
        public async Task<List<EspecialidadDto>> ObtenerEspecialidades()
        {
            return await _context.Especialidad
                .Select(e => new EspecialidadDto
                {
                    IdEspecialidad = e.IdEspecialidad,
                    NombreEspecialidad = e.NombreEspecialidad,
                    DescripcionEspecialidad = e.DescripcionEspecialidad
                })
                .ToListAsync();
        }
    }
}
