using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Facturacion;
using backMecatronic.Models.Entities.Notificaciones;
using backMecatronic.Models.Entities.Personal;
using backMecatronic.Models.Entities.Cotizacion;

namespace backMecatronic.Models.Entities.Operaciones
{
    public class OrdenServicio
    {
        public int IdOrdenServicio { get; set; }
        public int? IdCliente { get; set; }
        public int? IdReserva { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProforma { get; set; }
        public int? IdVehiculo { get; set; }
        public int? IdTecnico { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal Total { get; set; }
        public TipoItemOrden TipoItem { get; set; }
        public string? Estado { get; set; }

        // Constructores
        public OrdenServicio() { } // Constructor para EF Core
        public OrdenServicio(int? idCliente, int? idReserva, int? idPedido, int? idProforma, int? vehiculoId, int? tecnicoId, DateTime fechaInicio, decimal total, string? estado)
        {
            IdCliente = idCliente;
            IdReserva = idReserva;
            IdPedido = idPedido;
            IdProforma = idProforma;
            IdVehiculo = vehiculoId;
            IdTecnico = tecnicoId;
            FechaInicio = fechaInicio;
            Total = total;
            Estado = estado;
        }

        // Navegación
        public Cliente? Cliente { get; set; }
        public Reserva? Reserva { get; set; }
        public Pedido? Pedido { get; set; }
        public Proforma? Proforma { get; set; }
        public Vehiculo? Vehiculo { get; set; }
        public Tecnico? Tecnico { get; set; }
        public ICollection<DetalleOrdenProducto> Detalles { get; set; } = new List<DetalleOrdenProducto>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();

    }
    public enum TipoItemOrden
    {
        Ninguno = 0,
        Servicio = 1,
        Producto = 2,
        Proforma = 3,
        Todo = 4
    }
}
