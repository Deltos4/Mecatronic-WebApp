namespace backMecatronic.Models.DTOs.Personal
{
    public class TecnicoDto
    {
        public int IdTecnico { get; set; }
        public string DniEmpleado { get; set; } = null!;
        public string CargoEmpleado { get; set; } = null!;
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
        public bool EstadoEmpleado { get; set; } = true;
    }

    public class TecnicoCreateDto
    {
        public string DniEmpleado { get; set; } = null!;
        public string CargoEmpleado { get; set; } = null!;
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
    }

    public class TecnicoUpdateDto
    {
        public string DniEmpleado { get; set; } = null!;
        public string CargoEmpleado { get; set; } = null!;
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
        public bool EstadoEmpleado { get; set; }
    }

    public class TecnicoResponseDto
    {
        public int IdTecnico { get; set; }
        public string DniEmpleado { get; set; } = null!;
        public string CargoEmpleado { get; set; } = null!;
        public string NombreEmpleado { get; set; } = null!;
        public string ApellidoEmpleado { get; set; } = null!;
        public string? TelefonoEmpleado { get; set; }
        public string? CorreoEmpleado { get; set; }
        public string? DireccionEmpleado { get; set; }
        public DateTime FechaContratacion { get; set; }
        public bool EstadoEmpleado { get; set; } = true;
        public List<EspecialidadDto> Especialidades { get; set; } = new List<EspecialidadDto>();
    }

    public class EspecialidadDto
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; } = null!;
        public string? DescripcionEspecialidad { get; set; }
    }

    public class DetalleTecnicoEspecialidadDto
    {
        public int IdDetalleTE { get; set; }
        public int IdTecnico { get; set; }
        public int IdEspecialidad { get; set; }
    }
}
