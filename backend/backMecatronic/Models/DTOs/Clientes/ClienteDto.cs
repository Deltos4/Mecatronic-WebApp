namespace backMecatronic.Models.DTOs.Clientes
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public int? IdUsuario { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; } = null!;
        public string NumeroDocumentoCliente { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool EstadoCliente { get; set; }
    }

    public class ClienteCreateDto
    {
        public int? IdUsuario { get; set; }
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumentoCliente { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
    }

    public class ClienteUpdateDto
    {
        public int IdTipoDocumento { get; set; }
        public string NumeroDocumentoCliente { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string ApellidoCliente { get; set; } = null!;
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
    }

    public class ClienteResponseDto
    {
        public int IdCliente { get; set; }
        public string? NombreCompleto { get; set; }
        public string? NombreTipoDocumento { get; set; }
        public string? NumeroDocumentoCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }
        public bool EstadoCliente { get; set; }
        public List<DetalleClienteVehiculoResponseDto> Vehiculos { get; set; } = new();
    }

    public class DetalleClienteVehiculoResponseDto
        {
            public int IdVehiculo { get; set; }
            public string NombreModelo { get; set; } = null!;
            public string NombreMarca { get; set; } = null!;
            public string NombreTipo { get; set; } = null!;
            public string PlacaVehiculo { get; set; } = null!;
            public int? AnioVehiculo { get; set; }
            public string? ColorVehiculo { get; set; }
            public string? UrlFotoVehiculo { get; set; }
            public bool EstadoVehiculo { get; set; }
    }
}
