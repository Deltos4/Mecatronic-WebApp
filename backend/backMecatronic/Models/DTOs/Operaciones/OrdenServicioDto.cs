namespace backMecatronic.Models.DTOs.Operaciones
{
    public class OrdenServicioDto
    {
        public int IdOrdenServicio { get; set; }
        public int? IdCliente { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProforma { get; set; }
        public int? IdVehiculo { get; set; }
        public string? PlacaVehiculo { get; set; }
        public int? IdTecnico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal Total { get; set; }
        public int TipoItem { get; set; }
        public string? Estado { get; set; }
    }

    public class OrdenServicioCreateDto
    {
        public int? IdCliente { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProforma { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdTecnico { get; set; }
        public DateTime FechaInicio { get; set; }
        public decimal Total { get; set; }
        public int TipoItem { get; set; }
    }

    public class OrdenServicioUpdateDto
    {
        public string? Estado { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class OrdenServicioResponseDto
    {
        public int IdOrdenServicio { get; set; }
        public int? IdCliente { get; set; }
        public string? NombreCliente { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProforma { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdTecnico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal Total { get; set; }
        public int TipoItem { get; set; }
        public List<DetalleOrdenResponseDto> DetalleOrden { get; set; } = new();
    }
    public class DetalleOrdenDto
    {
        public int IdDetalleOP { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class DetalleOrdenCreateDto
    {
        public int IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetalleOrdenUpdateDto
    {         
        public int IdDetalleOP { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetalleOrdenResponseDto
    {
        public int IdDetalleOP { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
