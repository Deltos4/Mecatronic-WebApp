using Microsoft.EntityFrameworkCore;
using backMecatronic.Data;
using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Models.Entities.Clientes;

namespace backMecatronic.Services.Clientes
{
    public class VehiculoService : IVehiculoService
    {
        private readonly AppDbContext _context;

        public VehiculoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehiculoDto>> ObtenerVehiculos()
        {
            return await _context.Vehiculo
                .Include(v => v.ModeloVehiculo)
                    .ThenInclude(m => m.MarcaVehiculo)
                .Include(v => v.ModeloVehiculo)
                    .ThenInclude(m => m.TipoVehiculo)
                .Select(v => new VehiculoDto
                {
                    IdVehiculo = v.IdVehiculo,
                    IdModeloVehiculo = v.IdModeloVehiculo,
                    NombreModelo = v.ModeloVehiculo.NombreModeloVehiculo,
                    NombreMarca = v.ModeloVehiculo.MarcaVehiculo.NombreMarcaVehiculo,
                    NombreTipo = v.ModeloVehiculo.TipoVehiculo.NombreTipoVehiculo,
                    PlacaVehiculo = v.PlacaVehiculo,
                    AnioVehiculo = v.AnioVehiculo,
                    ColorVehiculo = v.ColorVehiculo,
                    UrlFotoVehiculo = v.urlFotoVehiculo,
                    EstadoVehiculo = v.EstadoVehiculo
                })
                .ToListAsync();
        }

        public async Task<VehiculoDto?> ObtenerPorId(int id)
        {
            return await _context.Vehiculo
                .Where(v => v.IdVehiculo == id)
                .Include(v => v.ModeloVehiculo)
                    .ThenInclude(m => m.MarcaVehiculo)
                .Include(v => v.ModeloVehiculo)
                    .ThenInclude(m => m.TipoVehiculo)
                .Select(v => new VehiculoDto
                {
                    IdVehiculo = v.IdVehiculo,
                    IdModeloVehiculo = v.IdModeloVehiculo,
                    NombreModelo = v.ModeloVehiculo.NombreModeloVehiculo,
                    NombreMarca = v.ModeloVehiculo.MarcaVehiculo.NombreMarcaVehiculo,
                    NombreTipo = v.ModeloVehiculo.TipoVehiculo.NombreTipoVehiculo,
                    PlacaVehiculo = v.PlacaVehiculo,
                    AnioVehiculo = v.AnioVehiculo,
                    ColorVehiculo = v.ColorVehiculo,
                    UrlFotoVehiculo = v.urlFotoVehiculo,
                    EstadoVehiculo = v.EstadoVehiculo
                })
                .FirstOrDefaultAsync();
        }

        public async Task<VehiculoDto> CrearVehiculo(VehiculoCreateDto dto)
        {
            var vehiculo = new Vehiculo
            {
                IdModeloVehiculo = dto.IdModeloVehiculo,
                PlacaVehiculo = dto.PlacaVehiculo,
                AnioVehiculo = dto.AnioVehiculo,
                ColorVehiculo = dto.ColorVehiculo,
                urlFotoVehiculo = dto.UrlFotoVehiculo,
                EstadoVehiculo = true
            };

            _context.Vehiculo.Add(vehiculo);
            await _context.SaveChangesAsync();

            return await ObtenerPorId(vehiculo.IdVehiculo)
                ?? throw new Exception("Error al crear vehículo");
        }

        public async Task<bool> ActualizarVehiculo(int id, VehiculoUpdateDto dto)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);

            if (vehiculo == null) return false;

            vehiculo.IdModeloVehiculo = dto.IdModeloVehiculo;
            vehiculo.PlacaVehiculo = dto.PlacaVehiculo;
            vehiculo.AnioVehiculo = dto.AnioVehiculo;
            vehiculo.ColorVehiculo = dto.ColorVehiculo;
            vehiculo.urlFotoVehiculo = dto.UrlFotoVehiculo;
            vehiculo.EstadoVehiculo = dto.EstadoVehiculo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculo.FindAsync(id);

            if (vehiculo == null) return false;

            _context.Vehiculo.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return true;
        }

        // Asignación
        public async Task<bool> AsignarVehiculoCliente(DetalleVehiculoClienteCreateDto dto)
        {
            var existe = await _context.DetalleVehiculoCliente
                .AnyAsync(x => x.IdVehiculo == dto.IdVehiculo && x.IdCliente == dto.IdCliente);

            if (existe) return false;

            var relacion = new DetalleVehiculoCliente
            {
                IdVehiculo = dto.IdVehiculo,
                IdCliente = dto.IdCliente,
                FechaRegistro = DateTime.Now,
                Observaciones = dto.Observaciones
            };

            _context.DetalleVehiculoCliente.Add(relacion);
            await _context.SaveChangesAsync();

            return true;
        }

        // Catálogos
        public async Task<List<MarcaVehiculoDto>> ObtenerMarcas()
        {
            return await _context.MarcaVehiculo
                .Select(m => new MarcaVehiculoDto
                {
                    IdMarcaVehiculo = m.IdMarcaVehiculo,
                    NombreMarcaVehiculo = m.NombreMarcaVehiculo,
                    DescripcionMarcaVehiculo = m.DescripcionMarcaVehiculo
                })
                .ToListAsync();
        }

        public async Task<List<TipoVehiculoDto>> ObtenerTipos()
        {
            return await _context.TipoVehiculo
                .Select(t => new TipoVehiculoDto
                {
                    IdTipoVehiculo = t.IdTipoVehiculo,
                    NombreTipoVehiculo = t.NombreTipoVehiculo,
                    DescripcionTipoVehiculo = t.DescripcionTipoVehiculo
                })
                .ToListAsync();
        }

        public async Task<List<ModeloVehiculoDto>> ObtenerModelos()
        {
            return await _context.ModeloVehiculo
                .Include(m => m.MarcaVehiculo)
                .Include(m => m.TipoVehiculo)
                .Select(m => new ModeloVehiculoDto
                {
                    IdModeloVehiculo = m.IdModeloVehiculo,
                    IdTipoVehiculo = m.IdTipoVehiculo,
                    IdMarcaVehiculo = m.IdMarcaVehiculo,
                    NombreModeloVehiculo = m.NombreModeloVehiculo,
                    DescripcionModeloVehiculo = m.DescripcionModeloVehiculo
                })
                .ToListAsync();
        }

    }
}
