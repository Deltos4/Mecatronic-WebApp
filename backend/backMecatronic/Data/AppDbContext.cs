using backMecatronic.Models.Entities.Clientes;
using backMecatronic.Models.Entities.Cotizacion;
using backMecatronic.Models.Entities.Facturacion;
using backMecatronic.Models.Entities.Inventario;
using backMecatronic.Models.Entities.Maestros;
using backMecatronic.Models.Entities.Notificaciones;
using backMecatronic.Models.Entities.Operaciones;
using backMecatronic.Models.Entities.Personal;
using backMecatronic.Models.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;

namespace backMecatronic.Data
{
    public class AppDbContext : DbContext // Heredar de DbContext
    {
        // Constructor que recibe opciones de configuración
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Definir DbSet para cada entidad (tabla)
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<TipoVehiculo> TipoVehiculo { get; set; }
        public DbSet<MarcaVehiculo> MarcaVehiculo { get; set; }
        public DbSet<ModeloVehiculo> ModeloVehiculo { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<DetalleVehiculoCliente> DetalleVehiculoCliente { get; set; }
        public DbSet<Servicio> Servicio { get; set; }
        public DbSet<Reserva> Reserva { get; set; }
        public DbSet<DetalleReservaServicio> DetalleReservaServicio { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<MarcaProducto> MarcaProducto { get; set; }
        public DbSet<CategoriaProducto> CategoriaProducto { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Movimiento> Movimiento { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<DetallePedidoProducto> DetallePedidoProducto { get; set; }
        public DbSet<Especialidad> Especialidad { get; set; }
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<DetalleTecnicoEspecialidad> DetalleTecnicoEspecialidad { get; set; }
        public DbSet<MetodoPago> MetodoPago { get; set; }
        public DbSet<Pago> Pago { get; set; }
        public DbSet<TipoComprobante> TipoComprobante { get; set; }
        public DbSet<Comprobante> Comprobante { get; set; }
        public DbSet<ComprobanteDetalle> DetalleComprobante { get; set; }
        public DbSet<OrdenServicio> OrdenServicio { get; set; }
        public DbSet<DetalleOrdenProducto> DetalleOrdenProducto { get; set; }
        public DbSet<Proforma> Proforma { get; set; }
        public DbSet<DetalleProforma> DetalleProforma { get; set; }
        public DbSet<Notificacion> Notificacion { get; set; }

        // Configurar las relaciones, detalles y restricciones de las entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.Property(e => e.NombreRol)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionRol)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoRol)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.Usuarios)
                    .WithOne(u => u.Rol)
                    .HasForeignKey(u => u.IdRol)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.HasOne(e => e.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(e => e.IdRol)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.CorreoUsuario)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.ContrasenaUsuario)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.TelefonoUsuario)
                    .HasMaxLength(20);
                entity.Property(e => e.DireccionUsuario)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoUsuario)
                    .HasDefaultValue(true);
                entity.Property(e => e.ResetToken)
                    .HasMaxLength(255);
                entity.Property(e => e.ResetTokenExpiracion);

                entity.HasOne(e => e.Cliente)
                    .WithOne(c => c.Usuario)
                    .HasForeignKey<Cliente>(c => c.IdUsuario)
                    .IsRequired(false);

            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumento);

                entity.Property(e => e.NombreTipoDocumento)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionTipoDocumento)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Clientes)
                    .WithOne(c => c.TipoDocumento)
                    .HasForeignKey(c => c.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Proveedores)
                    .WithOne(p => p.TipoDocumento)
                    .HasForeignKey(p => p.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.HasOne(e => e.Usuario)
                    .WithOne(u => u.Cliente)
                    .IsRequired(false);

                entity.HasOne(e => e.TipoDocumento)
                    .WithMany(t => t.Clientes)
                    .HasForeignKey(e => e.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.NumeroDocumentoCliente)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.ApellidoCliente)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.TelefonoCliente)
                    .HasMaxLength(20);
                entity.Property(e => e.DireccionCliente)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoCliente)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.DetalleVehiculos)
                    .WithOne(d => d.Cliente)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasMany(e => e.Reservas)
                    .WithOne(r => r.Cliente)
                    .HasForeignKey(r => r.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Pedidos)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey(p => p.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrdenesServicio)
                    .WithOne(o => o.Cliente)
                    .HasForeignKey(o => o.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdTipoVehiculo);

                entity.Property(e => e.NombreTipoVehiculo)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionTipoVehiculo)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Modelos)
                    .WithOne(v => v.TipoVehiculo)
                    .HasForeignKey(v => v.IdTipoVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MarcaVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdMarcaVehiculo);

                entity.Property(e => e.NombreMarcaVehiculo)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionMarcaVehiculo)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Modelos)
                    .WithOne(v => v.MarcaVehiculo)
                    .HasForeignKey(v => v.IdMarcaVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ModeloVehiculo>(entity =>
            {
                entity.HasKey(e => e.IdModeloVehiculo);

                entity.HasOne(e => e.TipoVehiculo)
                    .WithMany(t => t.Modelos)
                    .HasForeignKey(e => e.IdTipoVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MarcaVehiculo)
                    .WithMany(m => m.Modelos)
                    .HasForeignKey(e => e.IdMarcaVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.NombreModeloVehiculo)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionModeloVehiculo)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Vehiculos)
                    .WithOne(v => v.ModeloVehiculo)
                    .HasForeignKey(v => v.IdModeloVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo);

                entity.HasOne(e => e.ModeloVehiculo)
                    .WithMany(m => m.Vehiculos)
                    .HasForeignKey(e => e.IdModeloVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.PlacaVehiculo)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.AnioVehiculo)
                    .HasMaxLength(4);
                entity.Property(e => e.ColorVehiculo)
                    .HasMaxLength(50);
                entity.Property(e => e.urlFotoVehiculo)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoVehiculo)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.DetalleClientes)
                    .WithOne(d => d.Vehiculo)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrdenServicios)
                    .WithOne(o => o.Vehiculo)
                    .HasForeignKey(o => o.IdVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleVehiculoCliente>(entity =>
            {
                entity.HasKey(e => e.IdDetalleVC);

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.DetalleVehiculos)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehiculo)
                    .WithMany(v => v.DetalleClientes)
                    .HasForeignKey(e => e.IdVehiculo)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.IdServicio);

                entity.Property(e => e.NombreServicio)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.DescripcionServicio)
                    .HasMaxLength(255);
                entity.Property(e => e.PrecioServicio)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.DuracionServicio)
                    .IsRequired()
                    .HasDefaultValue(60);
                entity.Property(e => e.EstadoServicio)
                    .HasDefaultValue(true);
                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(255);

                entity.HasMany(e => e.DetalleReservas)
                    .WithOne(d => d.Servicio)
                    .HasForeignKey(d => d.IdServicio)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva);

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Reservas)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.FechaProgramada)
                    .IsRequired();
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Pendiente");

                entity.HasMany(e => e.Detalles)
                    .WithOne(d => d.Reserva)
                    .HasForeignKey(d => d.IdReserva)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleReservaServicio>(entity =>
            {
                entity.HasKey(e => e.IdDetalleRS);

                entity.HasOne(e => e.Reserva)
                    .WithMany(r => r.Detalles)
                    .HasForeignKey(e => e.IdReserva)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Servicio)
                    .WithMany(s => s.DetalleReservas)
                    .HasForeignKey(e => e.IdServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Subtotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Observaciones)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.HasOne(e => e.TipoDocumento)
                    .WithMany(t => t.Proveedores)
                    .HasForeignKey(e => e.IdTipoDocumento)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired(false);

                entity.Property(e => e.NombreProveedor)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.NumeroDocumentoProveedor)
                    .HasMaxLength(20);   
                entity.Property(e => e.ContactoProveedor)
                    .HasMaxLength(100);
                entity.Property(e => e.TelefonoProveedor)
                    .HasMaxLength(20);
                entity.Property(e => e.CorreoProveedor)
                    .HasMaxLength(100);
                entity.Property(e => e.DireccionProveedor)
                    .HasMaxLength(255);
                
                entity.Property(e => e.EstadoProveedor)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.Productos)
                    .WithOne(p => p.Proveedor)
                    .HasForeignKey(p => p.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MarcaProducto>(entity =>
            {
                entity.HasKey(e => e.IdMarcaProducto);

                entity.Property(e => e.NombreMarcaProducto)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionMarcaProducto)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoMarcaProducto)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.Productos)
                    .WithOne(p => p.MarcaProducto)
                    .HasForeignKey(p => p.IdMarcaProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaProducto);

                entity.Property(e => e.NombreCategoriaProducto)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionCategoriaProducto)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Productos)
                    .WithOne(p => p.CategoriaProducto)
                    .HasForeignKey(p => p.IdCategoriaProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.HasOne(e => e.Proveedor)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(e => e.IdProveedor)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MarcaProducto)
                    .WithMany(m => m.Productos)
                    .HasForeignKey(e => e.IdMarcaProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.CategoriaProducto)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(e => e.IdCategoriaProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.NombreProducto)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(255);
                entity.Property(e => e.PrecioProducto)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.ImagenUrl)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoProducto)
                    .HasDefaultValue(true);

                entity.HasOne(e => e.Stock)
                    .WithOne(c => c.Producto)
                    .HasForeignKey<Stock>(c => c.IdProducto)
                    .IsRequired(false);

                entity.HasMany(e => e.Movimientos)
                    .WithOne(d => d.Producto)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DetallesPedido)
                    .WithOne(d => d.Producto)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DetallesOrdenServicio)
                    .WithOne(d => d.Producto)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(e => e.IdStock);

                entity.HasOne(e => e.Producto)
                    .WithOne(u => u.Stock)
                    .IsRequired(true);

                entity.Property(e => e.CantidadActual)
                    .IsRequired(true);
                entity.Property(e => e.UnidadMedida)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasKey(e => e.IdMovimiento);

                entity.HasOne(e => e.Producto)
                    .WithMany(u => u.Movimientos)
                    .HasForeignKey(e => e.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.UnidadMedida)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Fecha)
                    .IsRequired();
                entity.Property(e => e.Referencia)
                    .IsRequired().
                    HasMaxLength(100);
            });

            modelBuilder.Entity <Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.Pedidos)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.TotalPedido)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.EstadoPedido)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("Pendiente");
                entity.Property(e => e.ObservacionesPedido)
                   .HasMaxLength(255);

                entity.HasMany(e => e.Detalles)
                    .WithOne(d => d.Pedido)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetallePedidoProducto>(entity =>
            {
                entity.HasKey(e => e.IdDetallePP);

                entity.HasOne(e => e.Pedido)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(e => e.IdPedido)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Producto)
                    .WithMany(p => p.DetallesPedido)
                    .HasForeignKey(e => e.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Subtotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad);

                entity.Property(e => e.NombreEspecialidad)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.DescripcionEspecialidad)
                    .HasMaxLength(255);

                entity.HasMany(e => e.DetallesTecnicoEspecialidad)
                    .WithOne(d => d.Especialidad)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Tecnico>(entity =>
            {
                entity.HasKey(e => e.IdTecnico);

                entity.Property(e => e.DniEmpleado)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.CargoEmpleado)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.ApellidoEmpleado)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.TelefonoEmpleado)
                    .HasMaxLength(20);
                entity.Property(e => e.CorreoEmpleado)
                    .HasMaxLength(100);
                entity.Property(e => e.DireccionEmpleado)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoEmpleado)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.DetallesTecnicoEspecialidad)
                    .WithOne(d => d.Tecnico)
                    .HasForeignKey(d => d.IdTecnico)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrdenServicios)
                    .WithOne(o => o.Tecnico)
                    .HasForeignKey(o => o.IdTecnico)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleTecnicoEspecialidad>(entity =>
            {
                entity.HasKey(e => e.IdDetalleTE);

                entity.HasOne(e => e.Tecnico)
                    .WithMany(t => t.DetallesTecnicoEspecialidad)
                    .HasForeignKey(e => e.IdTecnico)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Especialidad)
                    .WithMany(e => e.DetallesTecnicoEspecialidad)
                    .HasForeignKey(e => e.IdEspecialidad)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MetodoPago>(entity =>
            {
                entity.HasKey(e => e.IdMetodoPago);

                entity.Property(e => e.NombreMetodoPago)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.DescripcionMetodoPago)
                    .HasMaxLength(255);
                entity.Property(e => e.EstadoMetodoPago)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.Pagos)
                    .WithOne(p => p.MetodoPago)
                    .HasForeignKey(p => p.IdMetodoPago)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Pago>(entity =>
            {
                entity.HasKey(e => e.IdPago);

                entity.HasOne(e => e.OrdenServicio)
                    .WithMany(o => o.Pagos)
                    .HasForeignKey(e => e.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.MetodoPago)
                    .WithMany(m => m.Pagos)
                    .HasForeignKey(e => e.IdMetodoPago)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Monto)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<TipoComprobante>(entity =>
            {
                entity.HasKey(e => e.IdTipoComprobante);

                entity.Property(e => e.CodigoSunat)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255);

                entity.HasMany(e => e.Comprobantes)
                    .WithOne(c => c.TipoComprobante)
                    .HasForeignKey(c => c.IdTipoComprobante)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Comprobante>(entity =>
            {
                entity.HasKey(e => e.IdComprobante);

                entity.HasOne(e => e.OrdenServicio)
                    .WithMany(o => o.Comprobantes)
                    .HasForeignKey(e => e.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Operacion)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasOne(e => e.TipoComprobante)
                    .WithMany(t => t.Comprobantes)
                    .HasForeignKey(e => e.IdTipoComprobante)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.ClienteTipoDocumento)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.ClienteNumeroDocumento)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.ClienteDenominacion)
                    .IsRequired()
                    .HasMaxLength(150);
                entity.Property(e => e.Moneda)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.PorcentajeIgv)
                    .IsRequired()
                    .HasColumnType("decimal(5,2)")
                    .HasDefaultValue(18);
                entity.Property(e => e.TotalGravada)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.TotalIgv)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.EnviadoSunat)
                    .HasDefaultValue(false);

                entity.HasMany(e => e.Detalles)
                    .WithOne(d => d.Comprobante)
                    .HasForeignKey(d => d.IdComprobante)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Enlace)
                    .HasMaxLength(255);
                entity.Property(e => e.EnlacePdf)
                    .HasMaxLength(255);
                entity.Property(e => e.EnlaceXml)
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ComprobanteDetalle>(entity =>
            {
                entity.HasKey(e => e.IdComprobanteDetalle);

                entity.HasOne(e => e.Comprobante)
                    .WithMany(c => c.Detalles)
                    .HasForeignKey(e => e.IdComprobante)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.UnidadDeMedida)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.ValorUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Subtotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.TipoDeIgv)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.Igv)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.AnticipoRegularizacion)
                    .HasDefaultValue(false);
            });

            modelBuilder.Entity<OrdenServicio>(entity =>
            {
                entity.HasKey(e => e.IdOrdenServicio);

                entity.HasOne(e => e.Reserva)
                    .WithOne(r => r.OrdenServicio)
                    .HasForeignKey<OrdenServicio>(o => o.IdReserva)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Pedido)
                    .WithOne(p => p.OrdenServicio)
                    .HasForeignKey<OrdenServicio>(o => o.IdPedido)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Cliente)
                    .WithMany(c => c.OrdenesServicio)
                    .HasForeignKey(e => e.IdCliente)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Vehiculo)
                    .WithMany(v => v.OrdenServicios)
                    .HasForeignKey(e => e.IdVehiculo)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Tecnico)
                    .WithMany(t => t.OrdenServicios)
                    .HasForeignKey(e => e.IdTecnico)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.TipoItem)
                    .IsRequired()
                    .HasDefaultValue(TipoItemOrden.Servicio)
                    .HasSentinel(TipoItemOrden.Ninguno);
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValue("En Proceso");

                entity.HasMany(e => e.Detalles)
                    .WithOne(d => d.OrdenServicio)
                    .HasForeignKey(d => d.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Pagos)
                    .WithOne(p => p.OrdenServicio)
                    .HasForeignKey(p => p.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Comprobantes)
                    .WithOne(c => c.OrdenServicio)
                    .HasForeignKey(c => c.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleOrdenProducto>(entity =>
            {
                entity.HasKey(e => e.IdDetalleOP);

                entity.HasOne(e => e.OrdenServicio)
                    .WithMany(o => o.Detalles)
                    .HasForeignKey(e => e.IdOrdenServicio)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Producto)
                    .WithMany(p => p.DetallesOrdenServicio)
                    .HasForeignKey(e => e.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255);
                entity.Property(e => e.Cantidad)
                    .IsRequired()
                    .HasDefaultValue(1);
                entity.Property(e => e.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                entity.Property(e => e.Subtotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<Proforma>(entity =>
            {
                entity.HasKey(e => e.IdProforma);

                entity.HasOne(e => e.Cliente)
                    .WithMany(p => p.Proformas)
                    .HasForeignKey(e => e.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.FechaEmision)
                    .IsRequired();
                entity.Property(e => e.FechaVencimiento)
                    .IsRequired();
                entity.Property(e => e.Total)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

                entity.HasMany(e => e.Detalles)
                    .WithOne(p => p.Proforma)
                    .HasForeignKey(p => p.IdProforma)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.OrdenesServicio)
                    .WithOne(p => p.Proforma)
                    .HasForeignKey(p => p.IdProforma)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<DetalleProforma>(entity =>
            {
                entity.HasKey(e => e.IdDetalleProforma);

                entity.HasOne(e => e.Proforma)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(e => e.IdProforma)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Producto)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(e => e.IdProducto)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Servicio)
                    .WithMany(p => p.Detalles)
                    .HasForeignKey(e => e.IdServicio)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Cantidad)
                    .IsRequired();
                entity.Property(e => e.PrecioUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.SubTotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Notificacion>(entity =>
            {
                entity.HasKey(e => e.IdNotificacion);

                entity.HasOne(e => e.Usuario)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(e => e.IdUsuario)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Reserva)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(e => e.IdReserva)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Pedido)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(e => e.IdPedido)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.OrdenServicio)
                    .WithMany(p => p.Notificaciones)
                    .HasForeignKey(e => e.IdOrdenServicio)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);
                entity.Property(e => e.Mensaje)
                    .IsRequired();
                entity.Property(e => e.FechaCreacion)
                    .IsRequired();
                entity.Property(e => e.Leido)
                    .IsRequired();
            });
        }
    }
}
