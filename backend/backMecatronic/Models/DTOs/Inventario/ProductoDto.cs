namespace backMecatronic.Models.DTOs.Inventario
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public int IdMarcaProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? ImagenUrl { get; set; }
        public bool EstadoProducto { get; set; } = true;
    }

    public class ProductoCreateDto
    {
        public int IdMarcaProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? ImagenUrl { get; set; }
    }

    public class ProductoUpdateDto
    {
        public int IdMarcaProducto { get; set; }
        public int IdCategoriaProducto { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? ImagenUrl { get; set; }
        public bool EstadoProducto { get; set; }
    }

    public class ProductoResponseDto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string NombreMarcaProducto { get; set; } = null!;
        public string NombreCategoriaProducto { get; set; } = null!;
        public string? NombreProveedor { get; set; }
        public string? DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public string? ImagenUrl { get; set; }
        public bool EstadoProducto { get; set; }
    }

    public class MarcaProductoDto
    {
        public int IdMarcaProducto { get; set; }
        public string NombreMarcaProducto { get; set; } = null!;
        public string? DescripcionMarcaProducto { get; set; }
        public bool EstadoMarcaProducto { get; set; }
    }

    public class CategoriaProductoDto
    {
        public int IdCategoriaProducto { get; set; }
        public string NombreCategoriaProducto { get; set; } = null!;
        public string? DescripcionCategoriaProducto { get; set; }
    }

    public class StockDto
    {
        public int IdStock { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int CantidadActual { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public int? StockMinimo { get; set; }
        public string? Ubicacion { get; set; }
    }

    public class MovimientoDto
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Referencia { get; set; } = null!;
    }

    public class MovimientoCreateDto
    {
        public int IdProducto { get; set; }
        public string Tipo { get; set; } = null!; // "Entrada" o "Salida"
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; } = "UND";
        public string Referencia { get; set; } = null!;
    }
}
