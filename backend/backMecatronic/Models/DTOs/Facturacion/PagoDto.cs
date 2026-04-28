namespace backMecatronic.Models.DTOs.Facturacion
{
    public class PagoDto
    {
        public int IdPago { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
    }

    public class PagoCreateDto
    {
        public int IdOrdenServicio { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
    }

    public class PagoUpdateDto
    {
        public int IdPago { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdMetodoPago { get; set; }
        public decimal Monto { get; set; }
    }

    public class PagoResponseDto
    {
        public int IdPago { get; set; }
        public int IdOrdenServicio { get; set; }
        public int IdMetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Monto { get; set; }
        public string NombreMetodoPago { get; set; } = null!;
    }

    public class MetodoPagoDto
    {
        public int IdMetodoPago { get; set; }
        public string NombreMetodoPago { get; set; } = null!; // Efectivo, Tarjeta, Transferencia, Yape, Plin, etc.
        public string? DescripcionMetodoPago { get; set; }
        public bool EstadoMetodoPago { get; set; } = true;
    }

    public class MetodoPagoResponseDto
    {
        public int IdMetodoPago { get; set; }
        public string NombreMetodoPago { get; set; } = null!;
        public string? DescripcionMetodoPago { get; set; }
        public bool EstadoMetodoPago { get; set; }
    }
}
