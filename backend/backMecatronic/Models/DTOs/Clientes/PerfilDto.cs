namespace backMecatronic.Models.DTOs.Clientes
{
    public class PerfilClienteResponseDto
    {
        public int IdUsuario { get; set; }
        public int? IdCliente { get; set; }
        public string? NombreUsuario { get; set; }
        public string? CorreoUsuario { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string? NumeroDocumentoCliente { get; set; }
        public string? NombreCliente { get; set; }
        public string? ApellidoCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
    }

    public class PerfilClienteUpdateDto
    {
        public int IdTipoDocumento { get; set; }
        public string? NumeroDocumentoCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
    }
}
