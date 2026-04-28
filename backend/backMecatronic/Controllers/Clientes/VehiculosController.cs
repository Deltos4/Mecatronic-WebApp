using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Services.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _service;

        public VehiculosController(IVehiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiculos()
        {
            return Ok(await _service.ObtenerVehiculos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehiculo(int id)
        {
            var result = await _service.ObtenerPorId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(VehiculoCreateDto dto)
        {
            var result = await _service.CrearVehiculo(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, VehiculoUpdateDto dto)
        {
            var result = await _service.ActualizarVehiculo(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarVehiculo(id);
            if (!result) return NotFound();
            return Ok();
        }

        // Asignación

        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar(DetalleVehiculoClienteCreateDto dto)
        {
            var result = await _service.AsignarVehiculoCliente(dto);

            if (!result) return BadRequest("Ya existe relación");

            return Ok();
        }

        // Catálogos

        [HttpGet("marcas")]
        public async Task<IActionResult> GetMarcas()
        {
            return Ok(await _service.ObtenerMarcas());
        }

        [HttpGet("tipos")]
        public async Task<IActionResult> GetTipos()
        {
            return Ok(await _service.ObtenerTipos());
        }

        [HttpGet("modelos")]
        public async Task<IActionResult> GetModelos()
        {
            return Ok(await _service.ObtenerModelos());
        }
    }
}
