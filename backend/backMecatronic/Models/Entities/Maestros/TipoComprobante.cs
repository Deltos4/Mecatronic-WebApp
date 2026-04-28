using backMecatronic.Models.Entities.Facturacion;

namespace backMecatronic.Models.Entities.Maestros
{
    public class TipoComprobante
    {
        public int IdTipoComprobante { get; set; }
        public string CodigoSunat { get; set; } = null!; // 1: Factura, 3: Boleta, 7: Nota de Crédito, 8: Nota de Débito
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        // Constructores
        public TipoComprobante() { } // Constructor para EF Core
        public TipoComprobante(string codigoSunat, string nombre, string? descripcion = null)
        {
            CodigoSunat = codigoSunat;
            Nombre = nombre;
            Descripcion = descripcion;
        }

        // Navegación
        public ICollection<Comprobante> Comprobantes { get; set; } = new List<Comprobante>();
    }
}
