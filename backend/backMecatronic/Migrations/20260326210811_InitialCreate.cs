using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backMecatronic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categoria_producto",
                columns: table => new
                {
                    id_categoria_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_categoria_producto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_categoria_producto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categoria_producto", x => x.id_categoria_producto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "especialidad",
                columns: table => new
                {
                    id_especialidad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_especialidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_especialidad = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_especialidad", x => x.id_especialidad);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "marca_producto",
                columns: table => new
                {
                    id_marca_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_marca_producto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_marca_producto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_marca_producto = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marca_producto", x => x.id_marca_producto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "marca_vehiculo",
                columns: table => new
                {
                    id_marca_vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_marca_vehiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_marca_vehiculo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_marca_vehiculo", x => x.id_marca_vehiculo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "metodo_pago",
                columns: table => new
                {
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_metodo_pago = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_metodo_pago = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_metodo_pago = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_metodo_pago", x => x.id_metodo_pago);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rol",
                columns: table => new
                {
                    id_rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_rol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_rol = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_rol = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rol", x => x.id_rol);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "servicio",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_servicio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_servicio = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precio_servicio = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    duracion_servicio = table.Column<int>(type: "int", nullable: false, defaultValue: 60),
                    estado_servicio = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    imagen_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_servicio", x => x.id_servicio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tecnico",
                columns: table => new
                {
                    id_tecnico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    dni_empleado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cargo_empleado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre_empleado = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido_empleado = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono_empleado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_empleado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_empleado = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_contratacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado_empleado = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tecnico", x => x.id_tecnico);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipo_comprobante",
                columns: table => new
                {
                    id_tipo_comprobante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codigo_sunat = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipo_comprobante", x => x.id_tipo_comprobante);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipo_documento",
                columns: table => new
                {
                    id_tipo_documento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_tipo_documento = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_tipo_documento = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipo_documento", x => x.id_tipo_documento);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tipo_vehiculo",
                columns: table => new
                {
                    id_tipo_vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_tipo_vehiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_tipo_vehiculo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tipo_vehiculo", x => x.id_tipo_vehiculo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_rol = table.Column<int>(type: "int", nullable: false),
                    nombre_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_usuario = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contrasena_usuario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono_usuario = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_usuario = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado_usuario = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    reset_token = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    reset_token_expiracion = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_usuario", x => x.id_usuario);
                    table.ForeignKey(
                        name: "fk_usuario_rol_id_rol",
                        column: x => x.id_rol,
                        principalTable: "rol",
                        principalColumn: "id_rol",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_tecnico_especialidad",
                columns: table => new
                {
                    id_detalle_te = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_tecnico = table.Column<int>(type: "int", nullable: false),
                    id_especialidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_tecnico_especialidad", x => x.id_detalle_te);
                    table.ForeignKey(
                        name: "fk_detalle_tecnico_especialidad_especialidad_id_especialidad",
                        column: x => x.id_especialidad,
                        principalTable: "especialidad",
                        principalColumn: "id_especialidad",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_tecnico_especialidad_tecnico_id_tecnico",
                        column: x => x.id_tecnico,
                        principalTable: "tecnico",
                        principalColumn: "id_tecnico",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proveedor",
                columns: table => new
                {
                    id_proveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_proveedor = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_tipo_documento = table.Column<int>(type: "int", nullable: true),
                    numero_documento_proveedor = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    contacto_proveedor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono_proveedor = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    correo_proveedor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_proveedor = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_proveedor = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proveedor", x => x.id_proveedor);
                    table.ForeignKey(
                        name: "fk_proveedor_tipo_documento_id_tipo_documento",
                        column: x => x.id_tipo_documento,
                        principalTable: "tipo_documento",
                        principalColumn: "id_tipo_documento",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "modelo_vehiculo",
                columns: table => new
                {
                    id_modelo_vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_tipo_vehiculo = table.Column<int>(type: "int", nullable: false),
                    id_marca_vehiculo = table.Column<int>(type: "int", nullable: false),
                    nombre_modelo_vehiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_modelo_vehiculo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_modelo_vehiculo", x => x.id_modelo_vehiculo);
                    table.ForeignKey(
                        name: "fk_modelo_vehiculo_marca_vehiculo_id_marca_vehiculo",
                        column: x => x.id_marca_vehiculo,
                        principalTable: "marca_vehiculo",
                        principalColumn: "id_marca_vehiculo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_modelo_vehiculo_tipo_vehiculo_id_tipo_vehiculo",
                        column: x => x.id_tipo_vehiculo,
                        principalTable: "tipo_vehiculo",
                        principalColumn: "id_tipo_vehiculo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int", nullable: true),
                    id_tipo_documento = table.Column<int>(type: "int", nullable: false),
                    numero_documento_cliente = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombre_cliente = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido_cliente = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono_cliente = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_cliente = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado_cliente = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cliente", x => x.id_cliente);
                    table.ForeignKey(
                        name: "fk_cliente_tipo_documento_id_tipo_documento",
                        column: x => x.id_tipo_documento,
                        principalTable: "tipo_documento",
                        principalColumn: "id_tipo_documento",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_cliente_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_marca_producto = table.Column<int>(type: "int", nullable: false),
                    id_categoria_producto = table.Column<int>(type: "int", nullable: false),
                    id_proveedor = table.Column<int>(type: "int", nullable: true),
                    nombre_producto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion_producto = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precio_producto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    imagen_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_producto = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_producto", x => x.id_producto);
                    table.ForeignKey(
                        name: "fk_producto_categoria_producto_id_categoria_producto",
                        column: x => x.id_categoria_producto,
                        principalTable: "categoria_producto",
                        principalColumn: "id_categoria_producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_producto_marca_producto_id_marca_producto",
                        column: x => x.id_marca_producto,
                        principalTable: "marca_producto",
                        principalColumn: "id_marca_producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_producto_proveedor_id_proveedor",
                        column: x => x.id_proveedor,
                        principalTable: "proveedor",
                        principalColumn: "id_proveedor",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "vehiculo",
                columns: table => new
                {
                    id_vehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_modelo_vehiculo = table.Column<int>(type: "int", nullable: false),
                    placa_vehiculo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    anio_vehiculo = table.Column<int>(type: "int", maxLength: 4, nullable: true),
                    color_vehiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    url_foto_vehiculo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado_vehiculo = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vehiculo", x => x.id_vehiculo);
                    table.ForeignKey(
                        name: "fk_vehiculo_modelo_vehiculo_id_modelo_vehiculo",
                        column: x => x.id_modelo_vehiculo,
                        principalTable: "modelo_vehiculo",
                        principalColumn: "id_modelo_vehiculo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    fecha_pedido = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    total_pedido = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    estado_pedido = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "Pendiente")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observaciones_pedido = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pedido", x => x.id_pedido);
                    table.ForeignKey(
                        name: "fk_pedido_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "proforma",
                columns: table => new
                {
                    id_proforma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    fecha_emision = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_vencimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proforma", x => x.id_proforma);
                    table.ForeignKey(
                        name: "fk_proforma_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "reserva",
                columns: table => new
                {
                    id_reserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    fecha_programada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "Pendiente")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reserva", x => x.id_reserva);
                    table.ForeignKey(
                        name: "fk_reserva_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movimiento",
                columns: table => new
                {
                    id_movimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    tipo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    unidad_medida = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    referencia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_movimiento", x => x.id_movimiento);
                    table.ForeignKey(
                        name: "fk_movimiento_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    id_stock = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad_actual = table.Column<int>(type: "int", nullable: false),
                    unidad_medida = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    stock_minimo = table.Column<int>(type: "int", nullable: true),
                    ubicacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock", x => x.id_stock);
                    table.ForeignKey(
                        name: "fk_stock_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_vehiculo_cliente",
                columns: table => new
                {
                    id_detalle_vc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_vehiculo = table.Column<int>(type: "int", nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    fecha_registro = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_vehiculo_cliente", x => x.id_detalle_vc);
                    table.ForeignKey(
                        name: "fk_detalle_vehiculo_cliente_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_vehiculo_cliente_vehiculo_id_vehiculo",
                        column: x => x.id_vehiculo,
                        principalTable: "vehiculo",
                        principalColumn: "id_vehiculo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_pedido_producto",
                columns: table => new
                {
                    id_detalle_pp = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_pedido = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_pedido_producto", x => x.id_detalle_pp);
                    table.ForeignKey(
                        name: "fk_detalle_pedido_producto_pedido_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_pedido_producto_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_proforma",
                columns: table => new
                {
                    id_detalle_proforma = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_proforma = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: true),
                    id_servicio = table.Column<int>(type: "int", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    sub_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_proforma", x => x.id_detalle_proforma);
                    table.ForeignKey(
                        name: "fk_detalle_proforma_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_proforma_proforma_id_proforma",
                        column: x => x.id_proforma,
                        principalTable: "proforma",
                        principalColumn: "id_proforma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_proforma_servicio_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "servicio",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_reserva_servicio",
                columns: table => new
                {
                    id_detalle_rs = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_reserva = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    observaciones = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_reserva_servicio", x => x.id_detalle_rs);
                    table.ForeignKey(
                        name: "fk_detalle_reserva_servicio_reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_reserva_servicio_servicio_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "servicio",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orden_servicio",
                columns: table => new
                {
                    id_orden_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: true),
                    id_reserva = table.Column<int>(type: "int", nullable: true),
                    id_pedido = table.Column<int>(type: "int", nullable: true),
                    id_proforma = table.Column<int>(type: "int", nullable: true),
                    id_vehiculo = table.Column<int>(type: "int", nullable: true),
                    id_tecnico = table.Column<int>(type: "int", nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    tipo_item = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    estado = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, defaultValue: "En Proceso")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orden_servicio", x => x.id_orden_servicio);
                    table.ForeignKey(
                        name: "fk_orden_servicio_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orden_servicio_pedido_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orden_servicio_proforma_id_proforma",
                        column: x => x.id_proforma,
                        principalTable: "proforma",
                        principalColumn: "id_proforma",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orden_servicio_reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orden_servicio_tecnico_id_tecnico",
                        column: x => x.id_tecnico,
                        principalTable: "tecnico",
                        principalColumn: "id_tecnico",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orden_servicio_vehiculo_id_vehiculo",
                        column: x => x.id_vehiculo,
                        principalTable: "vehiculo",
                        principalColumn: "id_vehiculo",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "comprobante",
                columns: table => new
                {
                    id_comprobante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_orden_servicio = table.Column<int>(type: "int", nullable: false),
                    operacion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_tipo_comprobante = table.Column<int>(type: "int", nullable: false),
                    serie = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    cliente_tipo_documento = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    cliente_numero_documento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cliente_denominacion = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_emision = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    moneda = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    porcentaje_igv = table.Column<decimal>(type: "decimal(5,2)", nullable: false, defaultValue: 18m),
                    total_gravada = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    total_igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    enviado_sunat = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    enlace = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    aceptada_sunat = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    anulado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    enlace_pdf = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    enlace_xml = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comprobante", x => x.id_comprobante);
                    table.ForeignKey(
                        name: "fk_comprobante_orden_servicio_id_orden_servicio",
                        column: x => x.id_orden_servicio,
                        principalTable: "orden_servicio",
                        principalColumn: "id_orden_servicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_comprobante_tipo_comprobante_id_tipo_comprobante",
                        column: x => x.id_tipo_comprobante,
                        principalTable: "tipo_comprobante",
                        principalColumn: "id_tipo_comprobante",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_orden_producto",
                columns: table => new
                {
                    id_detalle_op = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_orden_servicio = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidad = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 1m),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_orden_producto", x => x.id_detalle_op);
                    table.ForeignKey(
                        name: "fk_detalle_orden_producto_orden_servicio_id_orden_servicio",
                        column: x => x.id_orden_servicio,
                        principalTable: "orden_servicio",
                        principalColumn: "id_orden_servicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_detalle_orden_producto_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "notificacion",
                columns: table => new
                {
                    id_notificacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_reserva = table.Column<int>(type: "int", nullable: true),
                    id_pedido = table.Column<int>(type: "int", nullable: true),
                    id_orden_servicio = table.Column<int>(type: "int", nullable: true),
                    titulo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mensaje = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_creacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    leido = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notificacion", x => x.id_notificacion);
                    table.ForeignKey(
                        name: "fk_notificacion_orden_servicio_id_orden_servicio",
                        column: x => x.id_orden_servicio,
                        principalTable: "orden_servicio",
                        principalColumn: "id_orden_servicio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_notificacion_pedido_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_notificacion_reserva_id_reserva",
                        column: x => x.id_reserva,
                        principalTable: "reserva",
                        principalColumn: "id_reserva",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_notificacion_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pago",
                columns: table => new
                {
                    id_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_orden_servicio = table.Column<int>(type: "int", nullable: false),
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false),
                    fecha_pago = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pago", x => x.id_pago);
                    table.ForeignKey(
                        name: "fk_pago_metodo_pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalTable: "metodo_pago",
                        principalColumn: "id_metodo_pago",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_pago_orden_servicio_id_orden_servicio",
                        column: x => x.id_orden_servicio,
                        principalTable: "orden_servicio",
                        principalColumn: "id_orden_servicio",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_comprobante",
                columns: table => new
                {
                    id_comprobante_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_comprobante = table.Column<int>(type: "int", nullable: false),
                    unidad_de_medida = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidad = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 1m),
                    valor_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    precio_unitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    tipo_de_igv = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    igv = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    anticipo_regularizacion = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_detalle_comprobante", x => x.id_comprobante_detalle);
                    table.ForeignKey(
                        name: "fk_detalle_comprobante_comprobante_id_comprobante",
                        column: x => x.id_comprobante,
                        principalTable: "comprobante",
                        principalColumn: "id_comprobante",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_cliente_id_tipo_documento",
                table: "cliente",
                column: "id_tipo_documento");

            migrationBuilder.CreateIndex(
                name: "ix_cliente_id_usuario",
                table: "cliente",
                column: "id_usuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_comprobante_id_orden_servicio",
                table: "comprobante",
                column: "id_orden_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_comprobante_id_tipo_comprobante",
                table: "comprobante",
                column: "id_tipo_comprobante");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_comprobante_id_comprobante",
                table: "detalle_comprobante",
                column: "id_comprobante");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_orden_producto_id_orden_servicio",
                table: "detalle_orden_producto",
                column: "id_orden_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_orden_producto_id_producto",
                table: "detalle_orden_producto",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_pedido_producto_id_pedido",
                table: "detalle_pedido_producto",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_pedido_producto_id_producto",
                table: "detalle_pedido_producto",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_proforma_id_producto",
                table: "detalle_proforma",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_proforma_id_proforma",
                table: "detalle_proforma",
                column: "id_proforma");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_proforma_id_servicio",
                table: "detalle_proforma",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_reserva_servicio_id_reserva",
                table: "detalle_reserva_servicio",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_reserva_servicio_id_servicio",
                table: "detalle_reserva_servicio",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_tecnico_especialidad_id_especialidad",
                table: "detalle_tecnico_especialidad",
                column: "id_especialidad");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_tecnico_especialidad_id_tecnico",
                table: "detalle_tecnico_especialidad",
                column: "id_tecnico");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_vehiculo_cliente_id_cliente",
                table: "detalle_vehiculo_cliente",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "ix_detalle_vehiculo_cliente_id_vehiculo",
                table: "detalle_vehiculo_cliente",
                column: "id_vehiculo");

            migrationBuilder.CreateIndex(
                name: "ix_modelo_vehiculo_id_marca_vehiculo",
                table: "modelo_vehiculo",
                column: "id_marca_vehiculo");

            migrationBuilder.CreateIndex(
                name: "ix_modelo_vehiculo_id_tipo_vehiculo",
                table: "modelo_vehiculo",
                column: "id_tipo_vehiculo");

            migrationBuilder.CreateIndex(
                name: "ix_movimiento_id_producto",
                table: "movimiento",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "ix_notificacion_id_orden_servicio",
                table: "notificacion",
                column: "id_orden_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_notificacion_id_pedido",
                table: "notificacion",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "ix_notificacion_id_reserva",
                table: "notificacion",
                column: "id_reserva");

            migrationBuilder.CreateIndex(
                name: "ix_notificacion_id_usuario",
                table: "notificacion",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_cliente",
                table: "orden_servicio",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_pedido",
                table: "orden_servicio",
                column: "id_pedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_proforma",
                table: "orden_servicio",
                column: "id_proforma");

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_reserva",
                table: "orden_servicio",
                column: "id_reserva",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_tecnico",
                table: "orden_servicio",
                column: "id_tecnico");

            migrationBuilder.CreateIndex(
                name: "ix_orden_servicio_id_vehiculo",
                table: "orden_servicio",
                column: "id_vehiculo");

            migrationBuilder.CreateIndex(
                name: "ix_pago_id_metodo_pago",
                table: "pago",
                column: "id_metodo_pago");

            migrationBuilder.CreateIndex(
                name: "ix_pago_id_orden_servicio",
                table: "pago",
                column: "id_orden_servicio");

            migrationBuilder.CreateIndex(
                name: "ix_pedido_id_cliente",
                table: "pedido",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "ix_producto_id_categoria_producto",
                table: "producto",
                column: "id_categoria_producto");

            migrationBuilder.CreateIndex(
                name: "ix_producto_id_marca_producto",
                table: "producto",
                column: "id_marca_producto");

            migrationBuilder.CreateIndex(
                name: "ix_producto_id_proveedor",
                table: "producto",
                column: "id_proveedor");

            migrationBuilder.CreateIndex(
                name: "ix_proforma_id_cliente",
                table: "proforma",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "ix_proveedor_id_tipo_documento",
                table: "proveedor",
                column: "id_tipo_documento");

            migrationBuilder.CreateIndex(
                name: "ix_reserva_id_cliente",
                table: "reserva",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "ix_stock_id_producto",
                table: "stock",
                column: "id_producto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_usuario_id_rol",
                table: "usuario",
                column: "id_rol");

            migrationBuilder.CreateIndex(
                name: "ix_vehiculo_id_modelo_vehiculo",
                table: "vehiculo",
                column: "id_modelo_vehiculo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalle_comprobante");

            migrationBuilder.DropTable(
                name: "detalle_orden_producto");

            migrationBuilder.DropTable(
                name: "detalle_pedido_producto");

            migrationBuilder.DropTable(
                name: "detalle_proforma");

            migrationBuilder.DropTable(
                name: "detalle_reserva_servicio");

            migrationBuilder.DropTable(
                name: "detalle_tecnico_especialidad");

            migrationBuilder.DropTable(
                name: "detalle_vehiculo_cliente");

            migrationBuilder.DropTable(
                name: "movimiento");

            migrationBuilder.DropTable(
                name: "notificacion");

            migrationBuilder.DropTable(
                name: "pago");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "comprobante");

            migrationBuilder.DropTable(
                name: "servicio");

            migrationBuilder.DropTable(
                name: "especialidad");

            migrationBuilder.DropTable(
                name: "metodo_pago");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "orden_servicio");

            migrationBuilder.DropTable(
                name: "tipo_comprobante");

            migrationBuilder.DropTable(
                name: "categoria_producto");

            migrationBuilder.DropTable(
                name: "marca_producto");

            migrationBuilder.DropTable(
                name: "proveedor");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "proforma");

            migrationBuilder.DropTable(
                name: "reserva");

            migrationBuilder.DropTable(
                name: "tecnico");

            migrationBuilder.DropTable(
                name: "vehiculo");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "modelo_vehiculo");

            migrationBuilder.DropTable(
                name: "tipo_documento");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "marca_vehiculo");

            migrationBuilder.DropTable(
                name: "tipo_vehiculo");

            migrationBuilder.DropTable(
                name: "rol");
        }
    }
}
