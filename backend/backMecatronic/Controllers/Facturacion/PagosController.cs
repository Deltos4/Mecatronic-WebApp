using backMecatronic.Models.DTOs.Facturacion;
using backMecatronic.Services.Facturacion;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Facturacion
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _service;

        public PagosController(IPagoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerPagos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pago = await _service.ObtenerPagoPorId(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Pagar(PagoCreateDto dto)
        {
            var result = await _service.RegistrarPago(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, PagoUpdateDto dto)
        {
            var result = await _service.ActualizarPago(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.EliminarPago(id);
            if (!result) return NotFound();
            return Ok();
        }

        // Métodos para métodos de pago

        [HttpGet("metodos")]
        public async Task<IActionResult> GetMetodosPago()
        {
            return Ok(await _service.ObtenerMetodosPago());
        }
    }
}
