namespace backMecatronic.Models.DTOs.Operaciones
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaPedido { get; set; }
        public decimal TotalPedido { get; set; }
        public string EstadoPedido { get; set; } = null!; // Pendiente, Procesando, Completado, Cancelado
        public string? ObservacionesPedido { get; set; }
    }

    public class PedidoCreateDto
    {
        public int IdCliente { get; set; }
        public string? ObservacionesPedido { get; set; }
        public List<DetallePedidoCreateDto> Detalles { get; set; } = new();
    }

    public class PedidoUpdateDto
    {
        public int IdPedido { get; set; }
        public string EstadoPedido { get; set; } = null!; // Pendiente, Procesando, Completado, Cancelado
        public string? ObservacionesPedido { get; set; }
    }

    public class PedidoResponseDto
    {
        public int IdPedido { get; set; }
        public string NombreCliente { get; set; } = null!;
        public DateTime FechaPedido { get; set; }
        public decimal TotalPedido { get; set; }
        public string EstadoPedido { get; set; } = null!;
        public List<DetallePedidoResponseDto> Detalles { get; set; } = new();
    }

    public class DetallePedidoDto
    {
        public int IdDetallePP { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class DetallePedidoCreateDto
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetallePedidoUpdateDto
    {
        public int IdDetallePP { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class DetallePedidoResponseDto
    {
        public string NombreProducto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
