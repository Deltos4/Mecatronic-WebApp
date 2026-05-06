using backMecatronic.Models.DTOs.Clientes;

namespace backMecatronic.Services.Clientes
{
    public interface IVehiculoService
    {
        Task<List<VehiculoDto>> ObtenerVehiculos();
        Task<VehiculoDto?> ObtenerPorId(int id);
        Task<List<VehiculoDto>> ObtenerVehiculosPorCliente(int idCliente);
        Task<VehiculoDto> CrearVehiculo(VehiculoCreateDto dto);
        Task<bool> ActualizarVehiculo(int id, VehiculoUpdateDto dto);
        Task<bool> EliminarVehiculo(int id);

        // Asignación
        Task<bool> AsignarVehiculoCliente(DetalleVehiculoClienteCreateDto dto);

        // Catálogos
        Task<List<MarcaVehiculoDto>> ObtenerMarcas();
        Task<List<TipoVehiculoDto>> ObtenerTipos();
        Task<List<ModeloVehiculoDto>> ObtenerModelos();
    }
}
