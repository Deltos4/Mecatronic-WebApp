using backMecatronic.Models.DTOs.Personal;
using backMecatronic.Services.Personal;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Personal
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly IServicioService _service;

        public ServiciosController(IServicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _service.ObtenerServicios();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var servicio = await _service.ObtenerServicioPorId(id);
            if (servicio == null) return NotFound();
            return Ok(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServicioCreateDto dto)
        {
            var servicio = await _service.CrearServicio(dto);
            return Ok(servicio);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ServicioUpdateDto dto)
        {
            var result = await _service.ActualizarServicio(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.EliminarServicio(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
