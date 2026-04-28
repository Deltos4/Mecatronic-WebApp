using backMecatronic.Models.DTOs.Inventario;
using backMecatronic.Services.Inventario;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Inventario
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorService _service;

        public ProveedoresController(IProveedorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            return Ok(await _service.ObtenerProveedores());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var result = await _service.ObtenerProveedorPorId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProveedorCreateDto dto)
        {
            var result = await _service.CrearProveedor(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProveedorUpdateDto dto)
        {
            var result = await _service.ActualizarProveedor(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarProveedor(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
