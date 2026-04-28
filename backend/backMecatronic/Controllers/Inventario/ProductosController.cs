using backMecatronic.Models.DTOs.Inventario;
using backMecatronic.Services.Inventario;
using Microsoft.AspNetCore.Mvc;

namespace backMecatronic.Controllers.Inventario
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.ObtenerProductos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _service.ObtenerProductoPorId(id);
            if (producto == null) return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDto dto)
        {
            var producto = await _service.CrearProducto(dto);
            return Ok(producto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProductoUpdateDto dto)
        {
            var result = await _service.ActualizarProducto(id, dto);
            if (!result) return NotFound();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.EliminarProducto(id);
            if (!result) return NotFound();
            return Ok();
        }

        // Catálogos
        [HttpGet("marcas")]
        public async Task<IActionResult> GetMarcas()
        {
            return Ok(await _service.ObtenerMarcas());
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            return Ok(await _service.ObtenerCategorias());
        }

        // Control de inventario
        [HttpGet("{id}/stock")]
        public async Task<IActionResult> GetStock(int id)
        {
            var stock = await _service.ObtenerStock(id);
            if (stock == null) return NotFound();
            return Ok(stock);
        }

        [HttpPost("movimientos")]
        public async Task<IActionResult> RegistrarMovimiento(MovimientoCreateDto dto)
        {
            var result = await _service.RegistrarMovimiento(dto);
            if (!result) return BadRequest();
            return Ok();
        }
    }
}
