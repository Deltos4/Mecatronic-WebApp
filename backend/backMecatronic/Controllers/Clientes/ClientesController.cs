using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Services.Clientes;
using backMecatronic.Services.External;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;
        private readonly IApiPeruService _apiPeruService;

        public ClientesController(IClienteService service, IApiPeruService apiPeruService)
        {
            _service = service;
            _apiPeruService = apiPeruService;
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            return Ok(await _service.ObtenerClientes());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var cliente = await _service.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();
            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ClienteCreateDto dto)
        {
            var cliente = await _service.CrearCliente(dto);
            return Ok(cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteUpdateDto dto)
        {
            var result = await _service.ActualizarCliente(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eñiminar(int id)
        {
            var result = await _service.EliminarCliente(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("consulta-dni/{dni}")]
        public async Task<IActionResult> ConsultarDni(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8 || !dni.All(char.IsDigit))
                return BadRequest(new { message = "DNI inválido." });

            var result = await _apiPeruService.ConsultarDni(dni);
            return Ok(result);
        }
    }
}
