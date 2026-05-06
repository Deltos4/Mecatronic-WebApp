using backMecatronic.Models.DTOs.Operaciones;
using backMecatronic.Services.Operaciones;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Operaciones
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _service;

        public ReservasController(IReservaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerReservas());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reserva = await _service.ObtenerReservaPorId(id);

            if (reserva == null)
                return NotFound();

            return Ok(reserva);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ReservaCreateDto dto)
        {
            var result = await _service.CrearReserva(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, ReservaUpdateDto dto)
        {
            var result = await _service.ActualizarReserva(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarReserva(id);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpPut("{id}/cancelar")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var result = await _service.CancelarReserva(id);
            if (result == null) return NotFound();
            if (result == false)
                return BadRequest(new { message = "Solo se pueden cancelar reservas en estado Pendiente." });

            return Ok();
        }
    }
}
