using Microsoft.EntityFrameworkCore;
using backMecatronic.Data;
using backMecatronic.Models.Entities.Seguridad;
using backMecatronic.Models.DTOs.Seguridad;
using backMecatronic.Models.Entities.Clientes;

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
            throw new InvalidOperationException("Correo ya registrado");

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

        await using var transaction = await _context.Database.BeginTransactionAsync();

        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();

        var (nombre, apellido) = SplitNombre(dto.NombreUsuario);

        var cliente = new Cliente
        {
            IdUsuario = usuario.IdUsuario,
            IdTipoDocumento = 1,
            NumeroDocumentoCliente = string.Empty,
            NombreCliente = nombre,
            ApellidoCliente = apellido,
            FechaRegistro = DateTime.UtcNow,
            EstadoCliente = true
        };

        _context.Cliente.Add(cliente);
        await _context.SaveChangesAsync();

        await transaction.CommitAsync();

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
            .Include(u => u.Cliente)
            .FirstOrDefaultAsync(u => u.CorreoUsuario == dto.CorreoUsuario);

        if (usuario == null)
            throw new KeyNotFoundException("Usuario no encontrado");

        var valid = BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.ContrasenaUsuario);

        if (!valid)
            throw new UnauthorizedAccessException("Contraseña incorrecta");

        var token = _jwtHelper.GenerateToken(usuario);

        return new AuthResponseDto
        {
            IdUsuario = usuario.IdUsuario,
            IdCliente = usuario.Cliente?.IdCliente,
            Token = token,
            NombreUsuario = usuario.NombreUsuario,
            CorreoUsuario = usuario.CorreoUsuario,
            Rol = usuario.Rol.NombreRol
        };
    }

    private static (string Nombre, string Apellido) SplitNombre(string nombreCompleto)
    {
        var cleaned = (nombreCompleto ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(cleaned))
            return ("", "");

        var partes = cleaned.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (partes.Length == 1)
            return (partes[0], "");

        return (partes[0], string.Join(' ', partes.Skip(1)));
    }
}
