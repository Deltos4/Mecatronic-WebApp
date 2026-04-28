namespace backMecatronic.Models.DTOs.Personal

{
    public class ServicioDto
    {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int DuracionServicio { get; set; }
        public bool EstadoServicio { get; set; }
        public string? ImagenUrl { get; set; }
    }

    public class ServicioCreateDto
    {
        public string NombreServicio { get; set; } = null!;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int DuracionServicio { get; set; } = 60;
        public string? ImagenUrl { get; set; }
    }

    public class ServicioUpdateDto
    {
        public string NombreServicio { get; set; } = null!;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int DuracionServicio { get; set; } = 60;
        public bool EstadoServicio { get; set; }
        public string? ImagenUrl { get; set; }
    }

    public class ServicioResponseDto
        {
        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public string? DescripcionServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int DuracionServicio { get; set; }
        public bool EstadoServicio { get; set; }
        public string? ImagenUrl { get; set; }
    }
}
