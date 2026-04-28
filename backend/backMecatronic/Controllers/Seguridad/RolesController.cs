using backMecatronic.Models.DTOs.Seguridad;
using backMecatronic.Services.Seguridad;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Seguridad
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolService _service;

        public RolesController(IRolService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerRoles());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _service.ObtenerRolPorId(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolCreateDto dto)
        {
            var rol = await _service.CrearRol(dto);
            return Ok(rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolUpdateDto dto)
        {
            var result = await _service.ActualizarRol(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.EliminarRol(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
