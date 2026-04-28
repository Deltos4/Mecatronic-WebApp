using backMecatronic.Data;
using backMecatronic.Models.DTOs.Inventario;
using backMecatronic.Models.Entities.Inventario;
using backMecatronic.Services.Inventario;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Inventario
{
    public class ProveedorService : IProveedorService
    {
        private readonly AppDbContext _context;

        public ProveedorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ProveedorDto>> ObtenerProveedores()
        {
            return await _context.Proveedor
                .Include(p => p.TipoDocumento)
                .Select(p => new ProveedorDto
                {
                    IdProveedor = p.IdProveedor,
                    NombreProveedor = p.NombreProveedor,
                    NombreTipoDocumento = p.TipoDocumento != null ? p.TipoDocumento.NombreTipoDocumento : null,
                    NumeroDocumentoProveedor = p.NumeroDocumentoProveedor,
                    ContactoProveedor = p.ContactoProveedor,
                    TelefonoProveedor = p.TelefonoProveedor,
                    CorreoProveedor = p.CorreoProveedor,
                    DireccionProveedor = p.DireccionProveedor,
                    EstadoProveedor = p.EstadoProveedor
                })
                .ToListAsync();
        }

        public async Task<ProveedorResponseDto?> ObtenerProveedorPorId(int id)
        {
            return await _context.Proveedor
                .Include(p => p.TipoDocumento)
                .Where(p => p.IdProveedor == id)
                .Select(p => new ProveedorResponseDto
                {
                    IdProveedor = p.IdProveedor,
                    NombreProveedor = p.NombreProveedor,
                    NombreTipoDocumento = p.TipoDocumento != null ? p.TipoDocumento.NombreTipoDocumento : null,
                    NumeroDocumentoProveedor = p.NumeroDocumentoProveedor,
                    ContactoProveedor = p.ContactoProveedor,
                    TelefonoProveedor = p.TelefonoProveedor,
                    CorreoProveedor = p.CorreoProveedor,
                    DireccionProveedor = p.DireccionProveedor,
                    EstadoProveedor = p.EstadoProveedor
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProveedorResponseDto> CrearProveedor(ProveedorCreateDto dto)
        {
            var proveedor = new Proveedor
            {
                NombreProveedor = dto.NombreProveedor,
                IdTipoDocumento = dto.IdTipoDocumento,
                NumeroDocumentoProveedor = dto.NumeroDocumentoProveedor,
                ContactoProveedor = dto.ContactoProveedor,
                TelefonoProveedor = dto.TelefonoProveedor,
                CorreoProveedor = dto.CorreoProveedor,
                DireccionProveedor = dto.DireccionProveedor,
                EstadoProveedor = true
            };

            _context.Proveedor.Add(proveedor);
            await _context.SaveChangesAsync();

            return new ProveedorResponseDto
            {
                IdProveedor = proveedor.IdProveedor,
                NombreProveedor = proveedor.NombreProveedor,
                NombreTipoDocumento = (await _context.TipoDocumento.FindAsync(proveedor.IdTipoDocumento))?.NombreTipoDocumento,
                NumeroDocumentoProveedor = proveedor.NumeroDocumentoProveedor,
                ContactoProveedor = proveedor.ContactoProveedor,
                TelefonoProveedor = proveedor.TelefonoProveedor,
                CorreoProveedor = proveedor.CorreoProveedor,
                DireccionProveedor = proveedor.DireccionProveedor,
                EstadoProveedor = proveedor.EstadoProveedor
            };
        }

        public async Task<bool> ActualizarProveedor(int id, ProveedorUpdateDto dto)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);

            if (proveedor == null) return false;

            proveedor.NombreProveedor = dto.NombreProveedor;
            proveedor.IdTipoDocumento = dto.IdTipoDocumento;
            proveedor.NumeroDocumentoProveedor = dto.NumeroDocumentoProveedor;
            proveedor.ContactoProveedor = dto.ContactoProveedor;
            proveedor.TelefonoProveedor = dto.TelefonoProveedor;
            proveedor.CorreoProveedor = dto.CorreoProveedor;
            proveedor.DireccionProveedor = dto.DireccionProveedor;
            proveedor.EstadoProveedor = dto.EstadoProveedor;
;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarProveedor(int id)
        {
            var proveedor = await _context.Proveedor.FindAsync(id);

            if (proveedor == null) return false;

            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
