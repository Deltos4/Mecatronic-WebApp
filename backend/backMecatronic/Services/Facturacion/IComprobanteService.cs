using backMecatronic.Models.DTOs.Facturacion;
using backMecatronic.Models.Entities.Facturacion;

namespace backMecatronic.Services.Facturacion
{
    public interface IComprobanteService
    {
        Task<Comprobante> GenerarComprobante(int idOrden, string serie, int tipoComprobante);
    }
}
