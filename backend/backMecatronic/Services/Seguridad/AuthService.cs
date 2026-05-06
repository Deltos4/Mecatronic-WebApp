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
        if (dto == null)
            throw new ArgumentException("Datos de registro inválidos.");

        var correo = (dto.CorreoUsuario ?? string.Empty).Trim().ToLowerInvariant();
        var nombreUsuario = (dto.NombreUsuario ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nombreUsuario))
            throw new ArgumentException("El nombre y correo son obligatorios.");

        if (string.IsNullOrWhiteSpace(dto.Contrasena))
            throw new ArgumentException("La contraseña es obligatoria.");

        var rolId = dto.IdRol;
        if (rolId <= 0)
        {
            rolId = await _context.Rol
                .Where(r => r.NombreRol == "Cliente")
                .Select(r => r.IdRol)
                .FirstOrDefaultAsync();
        }

        if (rolId <= 0)
            throw new ArgumentException("Rol de cliente no disponible.");

        var existe = await _context.Usuario
            .AnyAsync(u => u.CorreoUsuario == correo);

        if (existe)
            throw new InvalidOperationException("Correo ya registrado");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena);

        var usuario = new Usuario
        {
            IdRol = rolId,
            NombreUsuario = nombreUsuario,
            CorreoUsuario = correo,
            ContrasenaUsuario = hash,
            EstadoUsuario = true,
            FechaRegistro = DateTime.UtcNow
        };

        var (nombre, apellido) = SplitNombre(nombreUsuario);

        var cliente = new Cliente
        {
            IdTipoDocumento = 1,
            NumeroDocumentoCliente = string.Empty,
            NombreCliente = nombre,
            ApellidoCliente = apellido,
            FechaRegistro = DateTime.UtcNow,
            EstadoCliente = true,
            Usuario = usuario
        };

        _context.Usuario.Add(usuario);
        _context.Cliente.Add(cliente);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("No se pudo registrar el usuario.", ex);
        }

        var rolNombre = await _context.Rol
            .Where(r => r.IdRol == rolId)
            .Select(r => r.NombreRol)
            .FirstOrDefaultAsync() ?? string.Empty;

        return new UsuarioResponseDto
        {
            IdUsuario = usuario.IdUsuario,
            NombreUsuario = usuario.NombreUsuario,
            CorreoUsuario = usuario.CorreoUsuario,
            Rol = rolNombre,
            EstadoUsuario = usuario.EstadoUsuario
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
