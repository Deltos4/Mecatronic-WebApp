using backMecatronic.Data;
using backMecatronic.Models.DTOs.Inventario;
using backMecatronic.Models.Entities.Inventario;
using backMecatronic.Services.Inventario;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Services
{
    public class ProductoService : IProductoService
    {
        private readonly AppDbContext _context;

        public ProductoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoResponseDto>> ObtenerProductos()
        {
            return await _context.Producto
                .Include(p => p.MarcaProducto)
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .Select(p => new ProductoResponseDto
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    NombreMarcaProducto = p.MarcaProducto.NombreMarcaProducto,
                    NombreCategoriaProducto = p.CategoriaProducto.NombreCategoriaProducto,
                    NombreProveedor = p.Proveedor != null ? p.Proveedor.NombreProveedor : null,
                    DescripcionProducto = p.DescripcionProducto,
                    PrecioProducto = p.PrecioProducto,
                    ImagenUrl = p.ImagenUrl,
                    EstadoProducto = p.EstadoProducto
                })
                .ToListAsync();
        }

        public async Task<ProductoResponseDto?> ObtenerProductoPorId(int id)
        {
            return await _context.Producto
                .Where(p => p.IdProducto == id)
                .Include(p => p.MarcaProducto)
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .Select(p => new ProductoResponseDto
                {
                    IdProducto = p.IdProducto,
                    NombreProducto = p.NombreProducto,
                    NombreMarcaProducto = p.MarcaProducto.NombreMarcaProducto,
                    NombreCategoriaProducto = p.CategoriaProducto.NombreCategoriaProducto,
                    NombreProveedor = p.Proveedor != null ? p.Proveedor.NombreProveedor : null,
                    DescripcionProducto = p.DescripcionProducto,
                    PrecioProducto = p.PrecioProducto,
                    ImagenUrl = p.ImagenUrl,
                    EstadoProducto = p.EstadoProducto
                })
                .FirstOrDefaultAsync();
        }

        public async Task<ProductoResponseDto> CrearProducto(ProductoCreateDto dto)
        {
            var producto = new Producto

            {
                IdMarcaProducto = dto.IdMarcaProducto,
                IdCategoriaProducto = dto.IdCategoriaProducto,
                IdProveedor = dto.IdProveedor,
                NombreProducto = dto.NombreProducto,
                DescripcionProducto = dto.DescripcionProducto,
                PrecioProducto = dto.PrecioProducto,
                ImagenUrl = dto.ImagenUrl
            };

            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

            var productoCreado = await _context.Producto
                .Include(p => p.MarcaProducto)
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.IdProducto == producto.IdProducto);

            return new ProductoResponseDto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                NombreMarcaProducto = producto.MarcaProducto.NombreMarcaProducto,
                NombreCategoriaProducto = producto.CategoriaProducto.NombreCategoriaProducto,
                NombreProveedor = producto.Proveedor != null ? producto.Proveedor.NombreProveedor : null,
                DescripcionProducto = producto.DescripcionProducto,
                PrecioProducto = producto.PrecioProducto,
                ImagenUrl = producto.ImagenUrl,
                EstadoProducto = producto.EstadoProducto
            };
        }

        public async Task<bool> ActualizarProducto(int id, ProductoUpdateDto dto)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
                return false;

            producto.IdMarcaProducto = dto.IdMarcaProducto;
            producto.IdCategoriaProducto = dto.IdCategoriaProducto;
            producto.IdProveedor = dto.IdProveedor;
            producto.NombreProducto = dto.NombreProducto;
            producto.DescripcionProducto = dto.DescripcionProducto;
            producto.PrecioProducto = dto.PrecioProducto;
            producto.ImagenUrl = dto.ImagenUrl;
            producto.EstadoProducto = dto.EstadoProducto;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EliminarProducto(int id)
        {
            var producto = await _context.Producto.FindAsync(id);

            if (producto == null)
                return false;

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return true;
        }

        // Catálogos
        public async Task<List<MarcaProductoDto>> ObtenerMarcas()
        {
            return await _context.MarcaProducto
                .Select(m => new MarcaProductoDto
                {
                    IdMarcaProducto = m.IdMarcaProducto,
                    NombreMarcaProducto = m.NombreMarcaProducto,
                    DescripcionMarcaProducto = m.DescripcionMarcaProducto,
                    EstadoMarcaProducto = m.EstadoMarcaProducto
                })
                .ToListAsync();
        }

        public async Task<List<CategoriaProductoDto>> ObtenerCategorias()
        {
            return await _context.CategoriaProducto
                .Select(c => new CategoriaProductoDto
                {
                    IdCategoriaProducto = c.IdCategoriaProducto,
                    NombreCategoriaProducto = c.NombreCategoriaProducto,
                    DescripcionCategoriaProducto = c.DescripcionCategoriaProducto
                })
                .ToListAsync();
        }

        // Métodos de control de inventario
        public async Task<StockDto?> ObtenerStock(int idProducto)
        {
            var stock = await _context.Stock
                .Where(s => s.IdProducto == idProducto)
                .Include(s => s.Producto)
                .FirstOrDefaultAsync();
            if (stock == null)
                return null;
            return new StockDto
            {
                IdStock = stock.IdStock,
                IdProducto = stock.IdProducto,
                NombreProducto = stock.Producto.NombreProducto,
                CantidadActual = stock.CantidadActual,
                UnidadMedida = stock.UnidadMedida,
                StockMinimo = stock.StockMinimo,
                Ubicacion = stock.Ubicacion
            };
        }

        public async Task<bool> RegistrarMovimiento(MovimientoCreateDto dto)
        {
            var stock = await _context.Stock
                .Where(s => s.IdProducto == dto.IdProducto)
                .FirstOrDefaultAsync();
            if (stock == null)
                return false;
            if (dto.Tipo == "Entrada")
            {
                stock.CantidadActual += dto.Cantidad;
            }
            else if (dto.Tipo == "Salida")
            {
                if (stock.CantidadActual < dto.Cantidad)
                    return false; // No hay suficiente stock para la salida
                stock.CantidadActual -= dto.Cantidad;
            }
            else
            {
                return false; // Tipo de movimiento no válido
            }
            var movimiento = new Movimiento
            {
                IdProducto = dto.IdProducto,
                Tipo = dto.Tipo,
                Cantidad = dto.Cantidad,
                UnidadMedida = dto.UnidadMedida,
                Referencia = dto.Referencia,
                Fecha = DateTime.UtcNow
            };
            _context.Movimiento.Add(movimiento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
