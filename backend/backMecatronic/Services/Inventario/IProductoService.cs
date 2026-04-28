using backMecatronic.Models.DTOs.Inventario;

namespace backMecatronic.Services.Inventario
{
    public interface IProductoService
    {
        Task<List<ProductoResponseDto>> ObtenerProductos();
        Task<ProductoResponseDto?> ObtenerProductoPorId(int id);
        Task<ProductoResponseDto> CrearProducto(ProductoCreateDto dto);
        Task<bool> ActualizarProducto(int id, ProductoUpdateDto dto);
        Task<bool> EliminarProducto(int id);

        // Catálogos
        Task<List<MarcaProductoDto>> ObtenerMarcas();
        Task<List<CategoriaProductoDto>> ObtenerCategorias();

        // Métodos de control de inventario
        Task<StockDto?> ObtenerStock(int idProducto); 
        Task<bool> RegistrarMovimiento(MovimientoCreateDto dto);
        
    }
}
