namespace backMecatronic.Models.Entities.Inventario
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdProducto { get; set; }
        public string Tipo { get; set; } = null!; // "Entrada" o "Salida"
        public int Cantidad { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Referencia { get; set; } = null!; // Referencia a la orden, reserva, etc.

        // Constructores
        public Movimiento() { } // Constructor para EF Core
        public Movimiento(int idProducto, string tipo, int cantidad, string unidadMedida, string referencia)
        {
            IdProducto = idProducto;
            Tipo = tipo;
            Cantidad = cantidad;
            UnidadMedida = unidadMedida;
            Referencia = referencia;
        }
        // Navegación
        public Producto Producto { get; set; } = null!;
    }
}
