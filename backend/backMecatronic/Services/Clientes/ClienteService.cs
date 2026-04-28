using backMecatronic.Data;
using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Services.Clientes;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services
{
    public class ClienteService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ClienteResponseDto>> ObtenerClientes()
        {
            return await _context.Cliente
                .Include(c => c.TipoDocumento)
                .Include(c => c.DetalleVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                        .ThenInclude(v => v.ModeloVehiculo)
                            .ThenInclude(m => m.MarcaVehiculo)
                .Include(c => c.DetalleVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                        .ThenInclude(v => v.ModeloVehiculo)
                            .ThenInclude(m => m.TipoVehiculo)
                .Select(c => new ClienteResponseDto
                {
                    IdCliente = c.IdCliente,
                    NombreCompleto = c.NombreCliente + " " + c.ApellidoCliente,
                    NombreTipoDocumento = c.TipoDocumento.NombreTipoDocumento,
                    NumeroDocumentoCliente = c.NumeroDocumentoCliente,
                    TelefonoCliente = c.TelefonoCliente,
                    DireccionCliente = c.DireccionCliente,
                    EstadoCliente = c.EstadoCliente,
                    Vehiculos = c.DetalleVehiculos.Select(cv => new DetalleClienteVehiculoResponseDto
                    {
                        IdVehiculo = cv.IdVehiculo,
                        NombreModelo = cv.Vehiculo.ModeloVehiculo.NombreModeloVehiculo,
                        NombreMarca = cv.Vehiculo.ModeloVehiculo.MarcaVehiculo.NombreMarcaVehiculo,
                        NombreTipo = cv.Vehiculo.ModeloVehiculo.TipoVehiculo.NombreTipoVehiculo,
                        PlacaVehiculo = cv.Vehiculo.PlacaVehiculo,
                        AnioVehiculo = cv.Vehiculo.AnioVehiculo,
                        ColorVehiculo = cv.Vehiculo.ColorVehiculo,
                        UrlFotoVehiculo = cv.Vehiculo.urlFotoVehiculo,
                        EstadoVehiculo = cv.Vehiculo.EstadoVehiculo
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ClienteResponseDto?> ObtenerClientePorId(int id)
        {
            return await _context.Cliente
                .Include(c => c.TipoDocumento)
                .Include(c => c.DetalleVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                        .ThenInclude(v => v.ModeloVehiculo)
                            .ThenInclude(m => m.MarcaVehiculo)
                .Include(c => c.DetalleVehiculos)
                    .ThenInclude(cv => cv.Vehiculo)
                        .ThenInclude(v => v.ModeloVehiculo)
                            .ThenInclude(m => m.TipoVehiculo)
                .Where(c => c.IdCliente == id)
                .Select(c => new ClienteResponseDto
                {
                    IdCliente = c.IdCliente,
                    NombreCompleto = c.NombreCliente + " " + c.ApellidoCliente,
                    NombreTipoDocumento = c.TipoDocumento.NombreTipoDocumento,
                    NumeroDocumentoCliente = c.NumeroDocumentoCliente,
                    TelefonoCliente = c.TelefonoCliente,
                    DireccionCliente = c.DireccionCliente,
                    EstadoCliente = c.EstadoCliente,
                    Vehiculos = c.DetalleVehiculos.Select(cv => new DetalleClienteVehiculoResponseDto
                    {
                        IdVehiculo = cv.IdVehiculo,
                        NombreModelo = cv.Vehiculo.ModeloVehiculo.NombreModeloVehiculo,
                        NombreMarca = cv.Vehiculo.ModeloVehiculo.MarcaVehiculo.NombreMarcaVehiculo,
                        NombreTipo = cv.Vehiculo.ModeloVehiculo.TipoVehiculo.NombreTipoVehiculo,
                        PlacaVehiculo = cv.Vehiculo.PlacaVehiculo,
                        AnioVehiculo = cv.Vehiculo.AnioVehiculo,
                        ColorVehiculo = cv.Vehiculo.ColorVehiculo,
                        UrlFotoVehiculo = cv.Vehiculo.urlFotoVehiculo,
                        EstadoVehiculo = cv.Vehiculo.EstadoVehiculo
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ClienteResponseDto> CrearCliente(ClienteCreateDto dto)
        {
            var cliente = new Cliente
            {
                IdUsuario = dto.IdUsuario,
                IdTipoDocumento = dto.IdTipoDocumento,
                NumeroDocumentoCliente = dto.NumeroDocumentoCliente,
                NombreCliente = dto.NombreCliente,
                ApellidoCliente = dto.ApellidoCliente,
                TelefonoCliente = dto.TelefonoCliente,
                DireccionCliente = dto.DireccionCliente,
                FechaRegistro = DateTime.UtcNow,
                EstadoCliente = true
            };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return new ClienteResponseDto
            {
                IdCliente = cliente.IdCliente,
                NombreCompleto = cliente.NombreCliente + " " + cliente.ApellidoCliente,
                NombreTipoDocumento = (await _context.TipoDocumento.FindAsync(cliente.IdTipoDocumento))?.NombreTipoDocumento,
                NumeroDocumentoCliente = cliente.NumeroDocumentoCliente,
                TelefonoCliente = cliente.TelefonoCliente,
                DireccionCliente = cliente.DireccionCliente,
                EstadoCliente = cliente.EstadoCliente
            };
        }

        public async Task<bool> ActualizarCliente(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null) return false;

            cliente.IdTipoDocumento = dto.IdTipoDocumento;
            cliente.NumeroDocumentoCliente = dto.NumeroDocumentoCliente;
            cliente.NombreCliente = dto.NombreCliente;
            cliente.ApellidoCliente = dto.ApellidoCliente;
            cliente.TelefonoCliente = dto.TelefonoCliente;
            cliente.DireccionCliente = dto.DireccionCliente;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null) return false;

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
