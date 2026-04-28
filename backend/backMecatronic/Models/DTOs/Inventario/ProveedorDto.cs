namespace backMecatronic.Models.DTOs.Inventario
{
    public class ProveedorDto
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public int? IdTipoDocumento { get; set; }
        public string? NombreTipoDocumento { get; set; }
        public string? NumeroDocumentoProveedor { get; set; }
        public string? ContactoProveedor { get; set; }
        public string? TelefonoProveedor { get; set; }
        public string? CorreoProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
        public bool EstadoProveedor { get; set; }
    }

    public class ProveedorCreateDto
    {
        public string NombreProveedor { get; set; } = null!;
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumentoProveedor { get; set; }
        public string? ContactoProveedor { get; set; }
        public string? TelefonoProveedor { get; set; }
        public string? CorreoProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
    }

    public class ProveedorUpdateDto
    {
        public string NombreProveedor { get; set; } = null!;
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumentoProveedor { get; set; }
        public string? ContactoProveedor { get; set; }
        public string? TelefonoProveedor { get; set; }
        public string? CorreoProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
        public bool EstadoProveedor { get; set; }
    }

    public class ProveedorResponseDto
    {
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public string? NombreTipoDocumento { get; set; }
        public string? NumeroDocumentoProveedor { get; set; }
        public string? ContactoProveedor { get; set; }
        public string? TelefonoProveedor { get; set; }
        public string? CorreoProveedor { get; set; }
        public string? DireccionProveedor { get; set; }
        public bool EstadoProveedor { get; set; }
    }
}
