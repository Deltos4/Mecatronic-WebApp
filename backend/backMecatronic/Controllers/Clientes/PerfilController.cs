using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backMecatronic.Data;
using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Models.Entities.Clientes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace backMecatronic.Controllers.Clientes
{
    [ApiController]
    [Route("api/perfil")]
    public class PerfilController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public PerfilController(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return Unauthorized();

            var usuario = await _context.Usuario
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

            if (usuario == null) return NotFound();

            var (nombre, apellido) = SplitNombre(usuario.NombreUsuario);
            var cliente = usuario.Cliente;

            return Ok(new PerfilClienteResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                IdCliente = cliente?.IdCliente,
                NombreUsuario = usuario.NombreUsuario,
                CorreoUsuario = usuario.CorreoUsuario,
                IdTipoDocumento = cliente?.IdTipoDocumento ?? 1,
                NumeroDocumentoCliente = cliente?.NumeroDocumentoCliente ?? string.Empty,
                NombreCliente = cliente?.NombreCliente ?? nombre,
                ApellidoCliente = cliente?.ApellidoCliente ?? apellido,
                TelefonoCliente = cliente?.TelefonoCliente,
                DireccionCliente = cliente?.DireccionCliente
            });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PerfilClienteUpdateDto dto)
        {
            var usuarioId = ObtenerUsuarioId();
            if (usuarioId == null) return Unauthorized();

            var usuario = await _context.Usuario
                .Include(u => u.Cliente)
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioId);

            if (usuario == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.NombreCliente))
                return BadRequest(new { message = "El nombre es obligatorio." });

            if (string.IsNullOrWhiteSpace(dto.ApellidoCliente))
                return BadRequest(new { message = "El apellido es obligatorio." });

            var cliente = usuario.Cliente;

            if (cliente == null)
            {
                cliente = new Cliente
                {
                    IdUsuario = usuario.IdUsuario,
                    IdTipoDocumento = dto.IdTipoDocumento == 0 ? 1 : dto.IdTipoDocumento,
                    NumeroDocumentoCliente = dto.NumeroDocumentoCliente?.Trim() ?? string.Empty,
                    NombreCliente = dto.NombreCliente.Trim(),
                    ApellidoCliente = dto.ApellidoCliente.Trim(),
                    TelefonoCliente = dto.TelefonoCliente?.Trim(),
                    DireccionCliente = dto.DireccionCliente?.Trim(),
                    FechaRegistro = DateTime.UtcNow,
                    EstadoCliente = true
                };

                _context.Cliente.Add(cliente);
            }
            else
            {
                cliente.IdTipoDocumento = dto.IdTipoDocumento == 0 ? cliente.IdTipoDocumento : dto.IdTipoDocumento;
                cliente.NumeroDocumentoCliente = dto.NumeroDocumentoCliente?.Trim() ?? cliente.NumeroDocumentoCliente;
                cliente.NombreCliente = dto.NombreCliente.Trim();
                cliente.ApellidoCliente = dto.ApellidoCliente.Trim();
                cliente.TelefonoCliente = dto.TelefonoCliente?.Trim();
                cliente.DireccionCliente = dto.DireccionCliente?.Trim();
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private int? ObtenerUsuarioId()
        {
            var authHeader = Request.Headers.Authorization.ToString();
            if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                return null;

            var token = authHeader["Bearer ".Length..].Trim();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? string.Empty);

            if (key.Length == 0) return null;

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                }, out _);

                var claim = principal.Claims.FirstOrDefault(c => c.Type == "IdUsuario");
                return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
            }
            catch
            {
                return null;
            }
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
}
