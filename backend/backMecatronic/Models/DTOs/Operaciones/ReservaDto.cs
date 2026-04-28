namespace backMecatronic.Models.DTOs.Operaciones
{
    public class ReservaDto
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = null!;
    }

    public class ReservaCreateDto
    {
        public int IdCliente { get; set; }
        public DateTime FechaProgramada { get; set; }
        public List<DetalleReservaCreateDto> Detalles { get; set; } = new();
    }

    public class ReservaUpdateDto
    {
        public int IdCliente { get; set; }
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = null!;
    }

    public class ReservaResponseDto
    {
        public int IdReserva { get; set; }
        public string NombreCliente { get; set; } = null!;
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = null!;
        public List<DetalleReservaResponseDto> Detalles { get; set; } = new();
    }

    public class DetalleReservaDto
    {
        public int IdDetalleRS { get; set; }
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string? Observaciones { get; set; }
    }

    public class DetalleReservaCreateDto
    {
        public int IdServicio { get; set; }
        public int Cantidad { get; set; } = 1;
        public decimal PrecioUnitario { get; set; }
        public string? Observaciones { get; set; }
    }

    public class DetalleReservaUpdateDto
    {
        public int IdDetalleRS { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string? Observaciones { get; set; }
    }

    public class DetalleReservaResponseDto
    {
        public string NombreServicio { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
        public string? Observaciones { get; set; }
    }
}
