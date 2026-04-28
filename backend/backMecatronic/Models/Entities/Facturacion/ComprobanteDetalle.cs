using System.Text.Json.Serialization;

namespace backMecatronic.Models.Entities.Facturacion
{
    public class ComprobanteDetalle
    {
        [JsonIgnore]
        public int IdComprobanteDetalle { get; set; }
        [JsonIgnore]
        public int IdComprobante { get; set; }
        // Datos obligatorios para generar el detalle del comprobante
        [JsonPropertyName("unidad_de_medida")]
        public string UnidadDeMedida { get; set; } = null!; // NIU = producto, ZZ = servicio
        [JsonPropertyName("codigo")]
        public string Codigo { get; set; } = null!;
        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = null!;
        [JsonPropertyName("cantidad")]
        public decimal Cantidad { get; set; }
        [JsonPropertyName("valor_unitario")]
        public decimal ValorUnitario { get; set; }
        [JsonPropertyName("precio_unitario")]
        public decimal PrecioUnitario { get; set; }
        [JsonPropertyName("subtotal")]
        public decimal Subtotal { get; set; }
        [JsonPropertyName("tipo_de_igv")]
        public int TipoDeIgv { get; set; } = 1;
        [JsonPropertyName("igv")]
        public decimal Igv { get; set; }
        [JsonPropertyName("total")]
        public decimal Total { get; set; }
        [JsonPropertyName("anticipo_regularizacion")]
        public bool AnticipoRegularizacion { get; set; } = false;

        // Constructores de datos obligatorios
        public ComprobanteDetalle() { } // Constructor para EF Core
        public ComprobanteDetalle(int comprobanteId, string unidadMedida, string codigo, string descripcion, decimal cantidad, decimal valorUnitario, decimal precioUnitario, decimal subtotal, int tipoIgv, decimal igv, decimal total)
        {
            IdComprobante = comprobanteId;
            UnidadDeMedida = unidadMedida;
            Codigo = codigo;
            Descripcion = descripcion;
            Cantidad = cantidad;
            ValorUnitario = valorUnitario;
            PrecioUnitario = precioUnitario;
            Subtotal = subtotal;
            TipoDeIgv = tipoIgv;
            Igv = igv;
            Total = total;
        }

        // Navegación
        [JsonIgnore]
        public Comprobante Comprobante { get; set; } = null!;
    }
}
