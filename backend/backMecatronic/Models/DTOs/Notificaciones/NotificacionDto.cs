namespace backMecatronic.Models.DTOs.Notificaciones
{
    public class NotificacionDto
    {
        public int IdNotificacion { get; set; }
        public int IdUsuario { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdOrdenServicio { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public bool Leido { get; set; }
    }

    public class NotificacionCreateDto
    {
        public int IdUsuario { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdOrdenServicio { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
    }
}
