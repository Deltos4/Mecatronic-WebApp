using backMecatronic.Models.DTOs.Maestros;
using backMecatronic.Services.Maestros;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Maestros
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaestrosController : ControllerBase
    {
        private readonly IMaestrosService _service;

        public MaestrosController(IMaestrosService service)
        {
            _service = service;
        }

        // Tipo de Documento
        [HttpGet("tipos-documento")]
        public async Task<IActionResult> GetTiposDocumento()
        {
            return Ok(await _service.ObtenerTiposDocumento());
        }

        [HttpGet("tipos-documento/{id}")]
        public async Task<IActionResult> GetTipoDocumento(int id)
        {
            var result = await _service.ObtenerTipoDocumentoPorId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("tipos-documento")]
        public async Task<IActionResult> CreateTipoDocumento(TipoDocumentoCreateDto dto)
        {
            var result = await _service.CrearTipoDocumento(dto);
            return Ok(result);
        }

        [HttpPut("tipos-documento/{id}")]
        public async Task<IActionResult> UpdateTipoDocumento(int id, TipoDocumentoUpdateDto dto)
        {
            var result = await _service.ActualizarTipoDocumento(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("tipos-documento/{id}")]
        public async Task<IActionResult> DeleteTipoDocumento(int id)
        {
            var result = await _service.EliminarTipoDocumento(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // Tipo de Comprobante
        [HttpGet("tipos-comprobante")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerTiposComprobante());
        }

        [HttpGet("tipos-comprobante/{id}")]
        public async Task<IActionResult> BetById(int id)
        {
            var result = await _service.ObtenerTipoComprobantePorId(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost("tipos-comprobante")]
        public async Task<IActionResult> Crear(TipoComprobanteCreateDto dto)
        {
            var result = await _service.CrearTipoComprobante(dto);
            return Ok(result);
        }

        [HttpPut("tipos-comprobante/{id}")]
        public async Task<IActionResult> Actualizar(int id, TipoComprobanteUpdateDto dto)
        {
            var result = await _service.ActualizarTipoComprobante(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("tipos-comprobante/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarTipoComprobante(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
