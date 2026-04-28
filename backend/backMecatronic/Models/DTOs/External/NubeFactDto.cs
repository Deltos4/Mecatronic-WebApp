using System.Text.Json.Serialization;

namespace backMecatronic.Models.DTOs.External
{
    public class NubeFactRequestDto
    {
        [JsonPropertyName("operacion")]
        public string Operacion { get; set; } = "generar_comprobante";

        [JsonPropertyName("tipo_de_comprobante")]
        public int TipoComprobante { get; set; }

        [JsonPropertyName("serie")]
        public string Serie { get; set; } = null!;

        [JsonPropertyName("numero")]
        public int Numero { get; set; }

        [JsonPropertyName("sunat_transaction")]
        public int SunatTransaction { get; set; } = 1;

        [JsonPropertyName("cliente_tipo_de_documento")]
        public int ClienteTipoDocumento { get; set; }

        [JsonPropertyName("cliente_numero_de_documento")]
        public string ClienteNumeroDocumento { get; set; } = null!;

        [JsonPropertyName("cliente_denominacion")]
        public string ClienteDenominacion { get; set; } = null!;

        [JsonPropertyName("fecha_de_emision")]
        public string FechaEmision { get; set; } = null!;

        [JsonPropertyName("moneda")]
        public int Moneda { get; set; } = 1; // 1 = Soles, 2 = Dólares, 3 = Euros, 4 = Libras Esterlinas

        [JsonPropertyName("porcentaje_de_igv")]
        public decimal PorcentajeIgv { get; set; }

        [JsonPropertyName("total_gravada")]
        public decimal TotalGravada { get; set; }

        [JsonPropertyName("total_igv")]
        public decimal TotalIgv { get; set; }

        [JsonPropertyName("total")]
        public decimal Total { get; set; }

        [JsonPropertyName("enviar_automaticamente_a_la_sumat")]
        public bool EnviadoSunat { get; set; }

        [JsonPropertyName("items")]
        public List<NubeFactItemDto> Items { get; set; } = new();
    }

    public class NubeFactItemDto
    {
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
    }

    public class NubeFactResponseDto
    {
        [JsonPropertyName("aceptada_por_sunat")]
        public bool Aceptada { get; set; }

        [JsonPropertyName("enlace")]
        public string Enlace { get; set; } = null!;

        [JsonPropertyName("enlace_del_pdf")]
        public string Pdf { get; set; } = null!;

        [JsonPropertyName("enlace_del_xml")]
        public string Xml { get; set; } = null!;

        [JsonPropertyName("mensaje")]
        public string? Mensaje { get; set; }

        [JsonPropertyName("errors")]
        public object? Errores { get; set; }
    }
}
