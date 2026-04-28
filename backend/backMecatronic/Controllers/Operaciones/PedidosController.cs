using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Services.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Operaciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidosController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerPedidos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pedido = await _service.ObtenerPedidoPorId(id);

            if (pedido == null)
                return NotFound();

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PedidoCreateDto dto)
        {
            var result = await _service.CrearPedido(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, PedidoUpdateDto dto)
        {
            var result = await _service.ActualizarPedido(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarPedido(id);
            if (!result) return NotFound();
            return Ok();
        }
    }
}
