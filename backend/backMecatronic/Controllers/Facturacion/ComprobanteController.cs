using backMecatronic.Models.DTOs.External;
using backMecatronic.Models.DTOs.Facturacion;
using backMecatronic.Services.External;
using backMecatronic.Services.Facturacion;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Facturacion
{

    [ApiController]
    [Route("api/[controller]")]
    public class ComprobantesController : ControllerBase
    {
        private readonly IComprobanteService _service;

        public ComprobantesController(IComprobanteService service)
        {
            _service = service;
        }

        [HttpPost("generar")]
        public async Task<IActionResult> Generar(int idOrden, string serie, int tipo)
        {
            try
            {
                var result = await _service.GenerarComprobante(idOrden, serie, tipo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
