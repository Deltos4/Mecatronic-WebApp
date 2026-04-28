namespace backMecatronic.Models.DTOs.Maestros
{
    // DTOs para TipoDocumento
    public class TipoDocumentoDto
    {
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; } = null!;
        public string? DescripcionTipoDocumento { get; set; }
    }

    public class TipoDocumentoCreateDto
    {
        public string NombreTipoDocumento { get; set; } = null!;
        public string? DescripcionTipoDocumento { get; set; }
    }

    public class TipoDocumentoUpdateDto
    {
        public string NombreTipoDocumento { get; set; } = null!;
        public string? DescripcionTipoDocumento { get; set; }
    }

    public class TipoDocumentoResponseDto
    {
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; } = null!;
        public string? DescripcionTipoDocumento { get; set; }
    }

    // DTOs para TipoComprobante
    public class TipoComprobanteDto
    {
        public int IdTipoComprobante { get; set; }
        public string CodigoSunat { get; set; } = null!; // 1: Factura, 3: Boleta, 7: Nota de Crédito, 8: Nota de Débito
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }

    public class TipoComprobanteCreateDto
    {
        public string CodigoSunat { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }

    public class TipoComprobanteUpdateDto
    {
        public string CodigoSunat { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }

    public class TipoComprobanteResponseDto
    {
        public int IdTipoComprobante { get; set; }
        public string CodigoSunat { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
