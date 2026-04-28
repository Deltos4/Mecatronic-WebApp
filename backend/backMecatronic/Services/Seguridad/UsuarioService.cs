using backMecatronic.Data;
using backMecatronic.Models.DTOs.Seguridad;
using backMecatronic.Models.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services.Seguridad
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioResponseDto>> ObtenerUsuarios()
        {
            return await _context.Usuario
                .Include(u => u.Rol)
                .Select(u => new UsuarioResponseDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    CorreoUsuario = u.CorreoUsuario,
                    Rol = u.Rol.NombreRol,
                    EstadoUsuario = u.EstadoUsuario
                })
                .ToListAsync();
        }

        public async Task<UsuarioResponseDto?> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuario
                .Include(u => u.Rol)
                .Where(u => u.IdUsuario == id)
                .Select(u => new UsuarioResponseDto
                {
                    IdUsuario = u.IdUsuario,
                    NombreUsuario = u.NombreUsuario,
                    CorreoUsuario = u.CorreoUsuario,
                    Rol = u.Rol.NombreRol,
                    EstadoUsuario = u.EstadoUsuario
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UsuarioResponseDto> CrearUsuario(UsuarioCreateDto dto)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.ContrasenaUsuario);

            var usuario = new Usuario
            {
                IdRol = dto.IdRol,
                NombreUsuario = dto.NombreUsuario,
                CorreoUsuario = dto.CorreoUsuario,
                ContrasenaUsuario = hash,
                TelefonoUsuario = dto.TelefonoUsuario,
                DireccionUsuario = dto.DireccionUsuario,
                FechaRegistro = DateTime.Now
            };

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                CorreoUsuario = usuario.CorreoUsuario,
                Rol = ""
            };
        }

        public async Task<bool> ActualizarUsuario(int id, UsuarioUpdateDto dto)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null) return false;

            usuario.IdRol = dto.IdRol;
            usuario.NombreUsuario = dto.NombreUsuario;
            usuario.CorreoUsuario = dto.CorreoUsuario;

            if (!string.IsNullOrEmpty(dto.ContrasenaUsuario))

            usuario.ContrasenaUsuario = BCrypt.Net.BCrypt.HashPassword(dto.ContrasenaUsuario);
            usuario.TelefonoUsuario = dto.TelefonoUsuario;
            usuario.DireccionUsuario = dto.DireccionUsuario;
            usuario.EstadoUsuario = dto.EstadoUsuario;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null) return false;

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
