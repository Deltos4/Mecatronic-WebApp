namespace backMecatronic.Models.Entities.Inventario
{
    public class Stock
    {
        public int IdStock { get; set; }
        public int IdProducto { get; set; }
        public int CantidadActual { get; set; }
        public string UnidadMedida { get; set; } = null!;
        public int? StockMinimo { get; set; }
        public string? Ubicacion { get; set; }

        // Constructores
        public Stock() { } // Constructor para EF Core
        public Stock(int idProducto, int cantidadActual, string unidadMedida, int? stockMinimo, string? ubicacion)
        {
            IdProducto = idProducto;
            CantidadActual = cantidadActual;
            UnidadMedida = unidadMedida;
            StockMinimo = stockMinimo;
            Ubicacion = ubicacion;
        }

        // Navegación
        public Producto Producto { get; set; } = null!;
    }
}
