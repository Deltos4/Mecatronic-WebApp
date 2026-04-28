using Microsoft.EntityFrameworkCore;
using backMecatronic.Data;
using backMecatronic.Models.DTOs.Maestros;
using backMecatronic.Models.Entities.Maestros;
using backMecatronic.Services.Maestros;

namespace backMecatronic.Services.Maestros
{
    public class MaestrosService : IMaestrosService
    {
        private readonly AppDbContext _context;

        public MaestrosService(AppDbContext context)
        {
            _context = context;
        }

        // Servicios para Tipo de Documento
        public async Task<List<TipoDocumentoDto>> ObtenerTiposDocumento()
        {
            return await _context.TipoDocumento
                .Select(td => new TipoDocumentoDto
                {
                    IdTipoDocumento = td.IdTipoDocumento,
                    NombreTipoDocumento = td.NombreTipoDocumento,
                    DescripcionTipoDocumento = td.DescripcionTipoDocumento
                })
                .ToListAsync();
        }

        public async Task<TipoDocumentoResponseDto?> ObtenerTipoDocumentoPorId(int id)
        { 
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);

            if (tipoDocumento == null) return null;

            return new TipoDocumentoResponseDto
            {
                IdTipoDocumento = tipoDocumento.IdTipoDocumento,
                NombreTipoDocumento = tipoDocumento.NombreTipoDocumento,
                DescripcionTipoDocumento = tipoDocumento.DescripcionTipoDocumento
            };
        }

        public async Task<TipoDocumentoResponseDto> CrearTipoDocumento(TipoDocumentoCreateDto dto)
        {
            var nuevoTipoDocumento = new TipoDocumento
            {
                NombreTipoDocumento = dto.NombreTipoDocumento,
                DescripcionTipoDocumento = dto.DescripcionTipoDocumento
            };

            _context.TipoDocumento.Add(nuevoTipoDocumento);
            await _context.SaveChangesAsync();

            return new TipoDocumentoResponseDto
            {
                IdTipoDocumento = nuevoTipoDocumento.IdTipoDocumento,
                NombreTipoDocumento = nuevoTipoDocumento.NombreTipoDocumento,
                DescripcionTipoDocumento = nuevoTipoDocumento.DescripcionTipoDocumento
            };
        }

        public async Task<bool> ActualizarTipoDocumento(int id, TipoDocumentoUpdateDto dto)
        {
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);

            if (tipoDocumento == null) return false;

            tipoDocumento.NombreTipoDocumento = dto.NombreTipoDocumento;
            tipoDocumento.DescripcionTipoDocumento = dto.DescripcionTipoDocumento;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarTipoDocumento(int id)
        {
            var tipoDocumento = await _context.TipoDocumento.FindAsync(id);

            if (tipoDocumento == null) return false;

            _context.TipoDocumento.Remove(tipoDocumento);
            await _context.SaveChangesAsync();

            return true;
        }

        // Servicios para Tipo de Comprobante
        public async Task<List<TipoComprobanteDto>> ObtenerTiposComprobante()
        {
            return await _context.TipoComprobante
                .Select(tc => new TipoComprobanteDto
                {
                    IdTipoComprobante = tc.IdTipoComprobante,
                    CodigoSunat = tc.CodigoSunat,
                    Nombre = tc.Nombre,
                    Descripcion = tc.Descripcion
                })
                .ToListAsync();
        }

        public async Task<TipoComprobanteResponseDto?> ObtenerTipoComprobantePorId(int id)
        {
            var tipoComprobante = await _context.TipoComprobante.FindAsync(id);
            if (tipoComprobante == null) return null;
            return new TipoComprobanteResponseDto
            {
                IdTipoComprobante = tipoComprobante.IdTipoComprobante,
                CodigoSunat = tipoComprobante.CodigoSunat,
                Nombre = tipoComprobante.Nombre,
                Descripcion = tipoComprobante.Descripcion
            };
        }

        public async Task<TipoComprobanteResponseDto> CrearTipoComprobante(TipoComprobanteCreateDto dto)
        {
            var tipocomprobante = new TipoComprobante
            {
                CodigoSunat = dto.CodigoSunat,
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            _context.TipoComprobante.Add(tipocomprobante);
            await _context.SaveChangesAsync();

            return new TipoComprobanteResponseDto
            {
                IdTipoComprobante = tipocomprobante.IdTipoComprobante,
                CodigoSunat = tipocomprobante.CodigoSunat,
                Nombre = tipocomprobante.Nombre,
                Descripcion = tipocomprobante.Descripcion
            };
        }
        public async Task<bool> ActualizarTipoComprobante(int id, TipoComprobanteUpdateDto dto)
        {
            var tipoComprobante = await _context.TipoComprobante.FindAsync(id);

            if (tipoComprobante == null) return false;

            tipoComprobante.CodigoSunat = dto.CodigoSunat;
            tipoComprobante.Nombre = dto.Nombre;
            tipoComprobante.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> EliminarTipoComprobante(int id)
        {
            var tipoComprobante = await _context.TipoComprobante.FindAsync(id);

            if (tipoComprobante == null) return false;

            _context.TipoComprobante.Remove(tipoComprobante);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
