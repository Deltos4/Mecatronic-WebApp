using backMecatronic.Data;
using backMecatronic.Services;
using backMecatronic.Services.Clientes;
using backMecatronic.Services.External;
using backMecatronic.Services.Facturacion;
using backMecatronic.Services.Inventario;
using backMecatronic.Services.Maestros;
using backMecatronic.Services.Notificaciones;
using backMecatronic.Services.Operaciones;
using backMecatronic.Services.Personal;
using backMecatronic.Services.Seguridad;
//using backMecatronic.Services.External;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// =====================
// DATABASE
// =====================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
    // De PascalCase a snake_case
    .UseSnakeCaseNamingConvention()
);

// =====================
// SERVICES (IMPORTANTE)
// =====================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IMaestrosService, MaestrosService>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();

builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<ITecnicoService, TecnicoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();

builder.Services.AddScoped<IProformaService, ProformaService>();

builder.Services.AddScoped<IOrdenServicioService, OrdenServicioService>();

builder.Services.AddScoped<IComprobanteService, ComprobanteService>();
builder.Services.AddScoped<IPagoService, PagoService>();

builder.Services.AddScoped<JwtHelper>();

builder.Services.AddHttpClient<INubeFactService, NubeFactService>();

// =====================
// CONTROLLERS
// =====================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// =====================
// OPENAPI / SCALAR
// =====================
builder.Services.AddOpenApi();

// =====================
// ANGULAR CORS
// =====================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// =====================
// BUILD APP
// =====================
var app = builder.Build();

// =====================
// MIDDLEWARE
// =====================
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.SortTagsAlphabetically();
        options.WithTitle("Mecatronic API");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAngularDev");

app.Run();