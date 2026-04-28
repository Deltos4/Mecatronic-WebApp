using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Services.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Operaciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProformasController : ControllerBase
    {
        private readonly IProformaService _service;
        public ProformasController(IProformaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerProformas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var proforma = await _service.ObtenerProformaPorId(id);

            if (proforma == null)
                return NotFound();

            return Ok(proforma);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProformaCreateDto dto)
        {
            var result = await _service.CrearProforma(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ProformaUpdateDto dto)
        {
            var result = await _service.ActualizarProforma(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarProforma(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
