using backMecatronic.Data;
using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Models.DTOs.Seguridad;
using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Seguridad;
using backMecatronic.Services.Seguridad;
using Microsoft.EntityFrameworkCore;

public class RolService : IRolService
{
    private readonly AppDbContext _context;

    public RolService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RolResponseDto>> ObtenerRoles()
    {
        return await _context.Rol
            .Select(c => new RolResponseDto
            {
                IdRol = c.IdRol,
                NombreRol = c.NombreRol,
                DescripcionRol = c.DescripcionRol,
                EstadoRol = c.EstadoRol
            })
            .ToListAsync();
    }

    public async Task<RolResponseDto?> ObtenerRolPorId(int id)
    {
        var rol = await _context.Rol.FindAsync(id);

        if (rol == null) return null;

        return new RolResponseDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol,
            DescripcionRol = rol.DescripcionRol,
            EstadoRol = rol.EstadoRol
        };
    }

    public async Task<RolResponseDto> CrearRol(RolCreateDto dto)
    {
        var rol = new Rol
        {
            NombreRol = dto.NombreRol,
            DescripcionRol = dto.DescripcionRol,
            EstadoRol = true
        };

        _context.Rol.Add(rol);
        await _context.SaveChangesAsync();

        return new RolResponseDto
        {
            IdRol = rol.IdRol,
            NombreRol = rol.NombreRol,
            DescripcionRol = rol.DescripcionRol,
            EstadoRol = rol.EstadoRol
        };
    }
    public async Task<bool> ActualizarRol(int id, RolUpdateDto dto)
    {
        var rol = await _context.Rol.FindAsync(id);

        if (rol == null) return false;

        rol.NombreRol = dto.NombreRol;
        rol.DescripcionRol = dto.DescripcionRol;
        rol.EstadoRol = dto.EstadoRol;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarRol(int id)
    {
        var rol = await _context.Rol.FindAsync(id);

        if (rol == null) return false;

        _context.Rol.Remove(rol);
        await _context.SaveChangesAsync();

        return true;
    }
}
