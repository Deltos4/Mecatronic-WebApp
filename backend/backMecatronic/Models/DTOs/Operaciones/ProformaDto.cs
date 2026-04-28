namespace backMecatronic.Models.DTOs.Operaciones
{
    public class ProformaDto
    {
        public int IdProforma { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Total { get; set; }
    }

    public class ProformaCreateDto
    {
        public int IdCliente { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public List<DetalleProformaCreateDto> Detalles { get; set; } = new();
    }

    public class ProformaUpdateDto
    {
        public int IdProforma { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }

    public class ProformaResponseDto
    {
        public int IdProforma { get; set; }
        public string NombreCliente { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal Total { get; set; }
        public List<DetalleProformaResponseDto> Detalles { get; set; } = new();
    }
    
    public class DetalleProformaDto
    {
        public int IdDetalleProforma { get; set; }
        public int? IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int? IdServicio { get; set; }
        public string? NombreServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class DetalleProformaCreateDto
    {
        public int? IdProducto { get; set; }
        public int? IdServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetalleProformaUpdateDto
    {
        public int IdDetalleProforma { get; set; }
        public int? IdProducto { get; set; }
        public int? IdServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetalleProformaResponseDto
    {
        public int IdDetalleProforma { get; set; }
        public string? NombreProducto { get; set; }
        public string? NombreServicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
