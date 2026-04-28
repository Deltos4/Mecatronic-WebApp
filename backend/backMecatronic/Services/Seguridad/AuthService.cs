using Microsoft.EntityFrameworkCore;
using backMecatronic.Data;
using backMecatronic.Models.Entities.Seguridad;
using backMecatronic.Models.DTOs.Seguridad;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtHelper _jwtHelper;

    public AuthService(AppDbContext context, JwtHelper jwtHelper)
    {
        _context = context;
        _jwtHelper = jwtHelper;
    }

    public async Task<UsuarioResponseDto> Registrar(RegisterDto dto)
    {
        var existe = await _context.Usuario
            .AnyAsync(u => u.CorreoUsuario == dto.CorreoUsuario);

        if (existe)
            throw new Exception("Correo ya registrado");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);

        var usuario = new Usuario
        {
            IdRol = dto.IdRol,
            NombreUsuario = dto.NombreUsuario,
            CorreoUsuario = dto.CorreoUsuario,
            ContrasenaUsuario = hash,
            EstadoUsuario = true,
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

    public async Task<AuthResponseDto> Login(LoginDto dto)
    {
        var usuario = await _context.Usuario
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.CorreoUsuario == dto.CorreoUsuario);

        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        var valid = BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.ContrasenaUsuario);

        if (!valid)
            throw new Exception("Contraseña incorrecta");

        var token = _jwtHelper.GenerateToken(usuario);

        return new AuthResponseDto
        {
            Token = token,
            NombreUsuario = usuario.NombreUsuario,
            Rol = usuario.Rol.NombreRol
        };
    }
}
