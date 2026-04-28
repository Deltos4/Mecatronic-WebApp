using backMecatronic.Models.DTOs.Personal;
using backMecatronic.Services.Personal;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Personal
{
    [ApiController]
    [Route("api/[controller]")]
    public class TecnicosController : ControllerBase
    {
        public readonly ITecnicoService _service;

        public TecnicosController(ITecnicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetTecnicos()
        {
            return Ok(await _service.ObtenerTecnicos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTecnico(int id)
        {
            var result = await _service.ObtenerTecnicoPorId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(TecnicoCreateDto dto)
        {
            var result = await _service.CrearTecnico(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, TecnicoUpdateDto dto)
        {
            var result = await _service.ActualizarTecnico(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarTecnico(id);
            if (!result) return NotFound();
            return Ok();
        }

        // Asignación

        [HttpPost("asignar")]
        public async Task<IActionResult> Asignar(DetalleTecnicoEspecialidadDto dto)
        {
            var result = await _service.AsignarEspecialidadTecnico(dto);

            if (!result) return BadRequest("Error al asignar la especialidad al técnico");

            return Ok();
        }

        // Catálogos

        [HttpGet("especialidades")]
        public async Task<IActionResult> GetEspecialidades()
        {
            return Ok(await _service.ObtenerEspecialidades());
        }
    }
}
