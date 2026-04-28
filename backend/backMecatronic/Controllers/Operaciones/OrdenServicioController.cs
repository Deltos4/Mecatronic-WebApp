using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Services.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Operaciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioService _service;

        public OrdenServicioController(IOrdenServicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerOrdenesServicio());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var orden = await _service.ObtenerOrdenServicioPorId(id);

            if (orden == null)
                return NotFound();

            return Ok(orden);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(OrdenServicioCreateDto dto)
        {
            var result = await _service.CrearOrdenServicio(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, OrdenServicioUpdateDto dto)
        {
            var result = await _service.ActualizarOrdenServicio(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarOrdenServicio(id);
            if (!result) return NotFound();
            return Ok();
        }

        // Detalles
        [HttpPost("{idOrden}/detalles")]
        public async Task<IActionResult> AgregarDetalle(int idOrden, DetalleOrdenCreateDto dto)
        {
            var result = await _service.AgregarDetalleOrden(idOrden, dto);
            if (!result) return NotFound();
            return Ok();
        }

        // Que funcione con pedido y/o reserva
        [HttpPost("desde-pedido/{idPedido}")]
        public async Task<IActionResult> CrearDesdePedido(int idPedido)
        {
            var result = await _service.CrearOrdenDesdePedido(idPedido);
            return Ok(result);
        }

        [HttpPost("desde-reserva/{idReserva}")]
        public async Task<IActionResult> CrearDesdeReserva(int idReserva)
        {
            var result = await _service.CrearOrdenDesdeReserva(idReserva);
            return Ok(result);
        }
         [HttpPost("desde-proforma/{idProforma}")]

        // Que funcione con proforma
        public async Task<IActionResult> CrearDesdeProforma(int idProforma)
        {
            var result = await _service.CrearOrdenDesdeProforma(idProforma);
            return Ok(result);
        }
    }
}
