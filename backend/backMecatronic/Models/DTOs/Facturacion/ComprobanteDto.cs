namespace backMecatronic.Models.DTOs.Facturacion
{
    public class ComprobanteGenerarDto
    {
        public int IdOrdenServicio { get; set; }
        public string TipoComprobante { get; set; } = "BOLETA"; // BOLETA o FACTURA
    }

    public class ComprobanteResponseDto
    {
        public int IdComprobante { get; set; }
        public int IdOrdenServicio { get; set; }
        public string Serie { get; set; } = null!;
        public int Numero { get; set; }
        public string Cliente { get; set; } = null!;
        public string Documento { get; set; } = null!;
        public decimal Total { get; set; }
        public string? EnlacePdf { get; set; }
        public string? EnlaceXml { get; set; }
        public bool AceptadaSunat { get; set; }
    }

    public class FacturacionRequestDto
    {
        public int IdOrdenServicio { get; set; }
        public int TipoComprobante { get; set; } // 1 factura, 2 boleta
        public string Serie { get; set; } = null!;
    }
}
