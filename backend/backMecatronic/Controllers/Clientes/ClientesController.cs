using backMecatronic.Models.DTOs.Clientes;
using backMecatronic.Services.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Clientes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
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
    }
}
