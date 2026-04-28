namespace backMecatronic.Models.DTOs.Clientes
{
    public class VehiculoDto
    {
        public int IdVehiculo { get; set; }
        public int IdModeloVehiculo { get; set; }
        public string NombreModelo { get; set; } = null!;
        public string NombreMarca { get; set; } = null!;
        public string NombreTipo { get; set; } = null!;
        public string PlacaVehiculo { get; set; } = null!;
        public int? AnioVehiculo { get; set; }
        public string? ColorVehiculo { get; set; }
        public string? UrlFotoVehiculo { get; set; }
        public bool EstadoVehiculo { get; set; }
    }

    public class VehiculoCreateDto
    {
        public int IdModeloVehiculo { get; set; }
        public string PlacaVehiculo { get; set; } = null!;
        public int? AnioVehiculo { get; set; }
        public string? ColorVehiculo { get; set; }
        public string? UrlFotoVehiculo { get; set; }
    }

    public class VehiculoUpdateDto
    {
        public int IdModeloVehiculo { get; set; }
        public string PlacaVehiculo { get; set; } = null!;
        public int? AnioVehiculo { get; set; }
        public string? ColorVehiculo { get; set; }
        public string? UrlFotoVehiculo { get; set; }
        public bool EstadoVehiculo { get; set; }
    }

    public class VehiculoResponseDto
    {
        public int IdVehiculo { get; set; }
        public int IdModeloVehiculo { get; set; }
        public string NombreModelo { get; set; } = null!;
        public string NombreMarca { get; set; } = null!;
        public string NombreTipo { get; set; } = null!;
        public string PlacaVehiculo { get; set; } = null!;
        public int? AnioVehiculo { get; set; }
        public string? ColorVehiculo { get; set; }
        public string? UrlFotoVehiculo { get; set; }
        public bool EstadoVehiculo { get; set; }
    }

    public class TipoVehiculoDto
    {
        public int IdTipoVehiculo { get; set; }
        public string NombreTipoVehiculo { get; set; } = null!;
        public string? DescripcionTipoVehiculo { get; set; }
    }

    public class MarcaVehiculoDto
    {
        public int IdMarcaVehiculo { get; set; }
        public string NombreMarcaVehiculo { get; set; } = null!;
        public string? DescripcionMarcaVehiculo { get; set; }
    }

    public class ModeloVehiculoDto
    {
        public int IdModeloVehiculo { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdMarcaVehiculo { get; set; }
        public string NombreModeloVehiculo { get; set; } = null!;
        public string? DescripcionModeloVehiculo { get; set; }
    }

    public class ModeloVehiculoCreateDto
    {
        public int IdTipoVehiculo { get; set; }
        public int IdMarcaVehiculo { get; set; }
        public string NombreModeloVehiculo { get; set; } = null!;
        public string? DescripcionModeloVehiculo { get; set; }
    }

    public class DetalleVehiculoClienteDto
    {
        public int IdDetalleVC { get; set; }
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }
        public string PlacaVehiculo { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public string? Observaciones { get; set; }
    }

    public class DetalleVehiculoClienteCreateDto
    {
        public int IdVehiculo { get; set; }
        public int IdCliente { get; set; }
        public string? Observaciones { get; set; }
    }
}
