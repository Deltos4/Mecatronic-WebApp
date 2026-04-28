using backMecatronic.Models.Entities.Maestros;
using backMecatronic.Models.Entities.Operaciones;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace backMecatronic.Models.Entities.Facturacion
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }
        public int IdOrdenServicio { get; set; }
        // Datos obligatorios para generar el comprobante
        [JsonPropertyName("operacion")]
        public string Operacion { get; set; } = null!; // Generar comprobante, Anular comprobante, etc.
        [JsonPropertyName("tipo_de_comprobante")]
        public int IdTipoComprobante { get; set; } // 1 = Factura, 2 = Boleta, etc.
        [JsonPropertyName("serie")]
        public string Serie { get; set; } = null!; // BBB1 = Boleta, FFF1 = Factura, etc.
        [JsonPropertyName("numero")]
        public int Numero { get; set; }
        [JsonPropertyName("cliente_tipo_de_documento")]
        public int ClienteTipoDocumento { get; set; } // 6 = RUC, 1 = DNI, etc.
        [JsonPropertyName("cliente_numero_de_documento")]
        public string ClienteNumeroDocumento { get; set; } = null!;
        [JsonPropertyName("cliente_denominacion")]
        public string ClienteDenominacion { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
        [JsonPropertyName("fecha_de_emision")]
        public string FechaEmisionFormateada => FechaEmision.ToString("dd-MM-yyyy");
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

        // Constructores de datos obligatorios
        public Comprobante() { } // Constructor para EF Core
        public Comprobante(int ordenServicio, int tipoComprobante, string serie, int numero, int clienteTipoDocumento, string clienteNumeroDocumento, string clienteDenominacion, int moneda, decimal totalGravada, decimal totalIgv, decimal total, bool enviado)
        {
            IdOrdenServicio = ordenServicio;
            Operacion = "generar_comprobante";
            IdTipoComprobante = tipoComprobante;
            Serie = serie;
            Numero = numero;
            ClienteTipoDocumento = clienteTipoDocumento;
            ClienteNumeroDocumento = clienteNumeroDocumento;
            ClienteDenominacion = clienteDenominacion;
            FechaEmision = DateTime.Now;
            Moneda = moneda;
            PorcentajeIgv = 18m;
            TotalGravada = totalGravada;
            TotalIgv = totalIgv;
            Total = total;
            EnviadoSunat = enviado;
        }

        // Respuesta NubeFact
        public string Enlace { get; set; } = null!;
        public bool AceptadaSunat { get; set; }
        public bool Anulado { get; set; } = false;
        public string EnlacePdf { get; set; } = null!;
        public string EnlaceXml { get; set; } = null!;

        // Constructores para actualizar datos después de la generación
        public void ActualizarRespuestaNubeFact(string enlace, bool aceptada, bool anulado, string enlacePdf, string enlaceXml)
        {
            Enlace = enlace;
            AceptadaSunat = aceptada;
            Anulado = anulado;
            EnlacePdf = enlacePdf;
            EnlaceXml = enlaceXml;
        }

        // Navegación
        public OrdenServicio OrdenServicio { get; set; } = null!;
        public TipoComprobante TipoComprobante { get; set; } = null!;
        [JsonPropertyName("items")]
        public ICollection<ComprobanteDetalle> Detalles { get; set; } = new List<ComprobanteDetalle>();
    }
}
