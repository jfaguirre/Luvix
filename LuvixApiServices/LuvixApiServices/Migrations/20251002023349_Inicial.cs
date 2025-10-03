using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LuvixApiServices.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasTienda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasTienda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposPublicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPublicacion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subcategorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoriaTiendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategorias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategorias_CategoriasTienda_CategoriaTiendaId",
                        column: x => x.CategoriaTiendaId,
                        principalTable: "CategoriasTienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "int", nullable: false),
                    PermisoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => new { x.RolId, x.PermisoId });
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos_PermisoId",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguidoresUsuarioAUsuario",
                columns: table => new
                {
                    IdSeguidor = table.Column<int>(type: "int", nullable: false),
                    IdSeguido = table.Column<int>(type: "int", nullable: false),
                    FechaSeguimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguidoresUsuarioAUsuario", x => new { x.IdSeguidor, x.IdSeguido });
                    table.ForeignKey(
                        name: "FK_SeguidoresUsuarioAUsuario_Usuarios_IdSeguido",
                        column: x => x.IdSeguido,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeguidoresUsuarioAUsuario_Usuarios_IdSeguidor",
                        column: x => x.IdSeguidor,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tiendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdCategoria = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Portada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tiendas_CategoriasTienda_IdCategoria",
                        column: x => x.IdCategoria,
                        principalTable: "CategoriasTienda",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tiendas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UsuarioId, x.RolId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mensajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuarioRemitente = table.Column<int>(type: "int", nullable: true),
                    IdTiendaRemitente = table.Column<int>(type: "int", nullable: true),
                    IdUsuarioDestinatario = table.Column<int>(type: "int", nullable: true),
                    IdTiendaDestinatario = table.Column<int>(type: "int", nullable: true),
                    MensajeTexto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Leido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensajes_Tiendas_IdTiendaDestinatario",
                        column: x => x.IdTiendaDestinatario,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajes_Tiendas_IdTiendaRemitente",
                        column: x => x.IdTiendaRemitente,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajes_Usuarios_IdUsuarioDestinatario",
                        column: x => x.IdUsuarioDestinatario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mensajes_Usuarios_IdUsuarioRemitente",
                        column: x => x.IdUsuarioRemitente,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Publicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTienda = table.Column<int>(type: "int", nullable: false),
                    IdTipoPublicacion = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publicaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Publicaciones_Tiendas_IdTienda",
                        column: x => x.IdTienda,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Publicaciones_TiposPublicacion_IdTipoPublicacion",
                        column: x => x.IdTipoPublicacion,
                        principalTable: "TiposPublicacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguidoresTiendaATienda",
                columns: table => new
                {
                    IdTiendaSeguidora = table.Column<int>(type: "int", nullable: false),
                    IdTiendaSeguida = table.Column<int>(type: "int", nullable: false),
                    FechaSeguimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguidoresTiendaATienda", x => new { x.IdTiendaSeguidora, x.IdTiendaSeguida });
                    table.ForeignKey(
                        name: "FK_SeguidoresTiendaATienda_Tiendas_IdTiendaSeguida",
                        column: x => x.IdTiendaSeguida,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SeguidoresTiendaATienda_Tiendas_IdTiendaSeguidora",
                        column: x => x.IdTiendaSeguidora,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SeguidoresUsuarioATienda",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdTienda = table.Column<int>(type: "int", nullable: false),
                    FechaSeguimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguidoresUsuarioATienda", x => new { x.IdUsuario, x.IdTienda });
                    table.ForeignKey(
                        name: "FK_SeguidoresUsuarioATienda_Tiendas_IdTienda",
                        column: x => x.IdTienda,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguidoresUsuarioATienda_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comentarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublicacion = table.Column<int>(type: "int", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    IdTienda = table.Column<int>(type: "int", nullable: true),
                    Contenido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comentarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comentarios_Publicaciones_IdPublicacion",
                        column: x => x.IdPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comentarios_Tiendas_IdTienda",
                        column: x => x.IdTienda,
                        principalTable: "Tiendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comentarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImagenesPublicacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPublicacion = table.Column<int>(type: "int", nullable: false),
                    UrlImagen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagenesPublicacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagenesPublicacion_Publicaciones_IdPublicacion",
                        column: x => x.IdPublicacion,
                        principalTable: "Publicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CategoriasTienda",
                columns: new[] { "Id", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Producto" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servicio" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Puede editar usuario", "EditarUsuario" },
                    { 2, "Puede eliminar usuarios", "EliminarUsuario" },
                    { 3, "Puede bloquear usuarios", "BloquearUsuario" },
                    { 4, "Puede desbloquear usuarios", "DesbloquearUsuario" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Editor" },
                    { 3, "Vendedor" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Apellido", "Email", "Estado", "FechaRegistro", "FotoPerfil", "Nombre", "Password" },
                values: new object[,]
                {
                    { 1, "Aguirre Aparicio", "jfaguirrex@outlook.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Juan Francisco", "Admin2025@" },
                    { 2, "Santana Martínez", "mesantana@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Margarita Elizabeth", "Admin2025@" },
                    { 3, "Castellanos Sánchez", "aycastellanos@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Andrea Yamileth", "Admin2025@" },
                    { 4, "García Melgar", "asgarcia@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Alisson Stefany", "Admin2025@" },
                    { 5, "Guerrero Mena", "frguerrero@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Fátima Roció", "Admin2025@" },
                    { 6, "Echegoyen Henríquez", "ryechegoyen@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Rodrigo Yohalmo", "Admin2025@" },
                    { 7, "López Ramírez", "odlopez@gmail.com", "activo", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Oscar Daniel", "Admin2025@" }
                });

            migrationBuilder.InsertData(
                table: "RolPermisos",
                columns: new[] { "PermisoId", "RolId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "Subcategorias",
                columns: new[] { "Id", "CategoriaTiendaId", "Descripcion", "FechaCreacion", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Frutas (manzanas, bananas, naranjas), verduras (lechuga, tomates, papas), carne, pescado.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos frescos" },
                    { 2, 1, "Tortas, pasteles, cupcakes, postres decorados y repostería artesanal.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pastelería" },
                    { 3, 1, "Collares, pulseras, aretes, anillos y accesorios decorativos asequibles.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bisutería" },
                    { 4, 1, "Herramientas, clavos, tornillos, pinturas, materiales eléctricos y construcción.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ferretería" },
                    { 5, 1, "Medicamentos, productos de higiene, primeros auxilios y artículos médicos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Farmacia" },
                    { 6, 1, "Prendas para hombres, mujeres y niños: camisas, pantalones, vestidos, ropa interior.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ropa" },
                    { 7, 1, "Zapatos, botas, sandalias, tenis y calzado deportivo o formal.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Calzado" },
                    { 8, 1, "Objetos hechos a mano: cerámica, madera, tejidos, decoración cultural.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Artesanías" },
                    { 9, 1, "Comidas tradicionales locales: tamales, pupusas, atoles, platos regionales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alimentos típicos" },
                    { 10, 1, "Leche, queso, yogur, mantequilla, crema y derivados lácteos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos lácteos" },
                    { 11, 1, "Dispositivos electrónicos como tablets, cámaras, drones y gadgets.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos tecnológicos" },
                    { 12, 1, "Venta de productos en grandes cantidades a bajo costo para revender.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tienda mayorista" },
                    { 13, 1, "Venta directa al consumidor final de productos variados.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tienda minorista" },
                    { 14, 1, "Especializada en la venta de pupusas rellenas con queso, frijol, chicharrón, etc.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pupusería" },
                    { 15, 1, "Juguetes para niños: muñecos, juegos educativos, rompecabezas, vehículos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juguetería" },
                    { 16, 1, "Telas, hilos, telares, productos para confección y diseño de ropa.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Textil" },
                    { 17, 1, "Sillas, mesas, camas, closets, muebles para hogar u oficina.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muebles" },
                    { 18, 1, "Cerveza artesanal, barriles, botellas, marcas locales e importadas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cervecería" },
                    { 19, 1, "Puertas, rejas, escaleras, techos y estructuras de acero o aluminio.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estructuras metálicas" },
                    { 20, 1, "Arroz, azúcar, frijoles, aceite, sal, fósforos, productos esenciales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos de la canasta básica" },
                    { 21, 1, "Relojes de pulsera, de pared, cronómetros y accesorios de tiempo.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Relojería" },
                    { 22, 1, "Establecimiento general que vende una variedad de productos de consumo diario.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tienda" },
                    { 23, 1, "Pizzas al horno, porciones, familiares, con diferentes ingredientes y sabores.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pizzería" },
                    { 24, 1, "Impresoras láser, inalámbricas, multifuncionales y suministros (tinta, papel).", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Impresoras" },
                    { 25, 1, "Laptops, PCs, componentes, monitores, teclados y periféricos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computadoras" },
                    { 26, 1, "Tacos, burritos, quesadillas, tortillas frescas y salsas caseras.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taquería" },
                    { 27, 1, "Café, capuchino, expresso, pastelillos, jugos naturales y snacks.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cafetería" },
                    { 28, 1, "Venta al por mayor de telas para confección, moda o tapicería.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Depósito de telas" },
                    { 29, 1, "Refrescos, jugos, agua embotellada, cerveza, energizantes en volumen.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Depósito de bebidas" },
                    { 30, 1, "Gran establecimiento que ofrece alimentos, limpieza, ropa y más.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Supermercado" },
                    { 31, 1, "Hamburguesas, hot dogs, papas fritas, pollo frito, comida para llevar.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comida rápida" },
                    { 32, 1, "Chocolates finos, bombones, trufas, barras y dulces hechos a mano.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chocolate artesanal" },
                    { 33, 1, "Huevos, pollo, gallinas, pavos y subproductos avícolas frescos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos avícolas" },
                    { 34, 1, "Jabón, champú, cepillo dental, toallas higiénicas, desodorante.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Higiene y cuidado personal" },
                    { 35, 1, "Pan, bollos, pan dulce, pan de molde, baguettes y productos horneados.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Panadería" },
                    { 36, 1, "Refrigeradores, estufas, licuadoras, freidoras, lavadoras, microondas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electrodomésticos" },
                    { 37, 1, "Utensilios, ollas, sartenes, platos, cubiertos y herramientas de cocina.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Artículos de cocina" },
                    { 38, 1, "Decoración interior, lámparas, cuadros, alfombras, muebles pequeños.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Muebles y decoración" },
                    { 39, 1, "Gorras, bolsos, cinturones, lentes de sol, bufandas y llaveros.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accesorios" },
                    { 40, 1, "Celulares, smartphones, accesorios, fundas, cargadores, audífonos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dispositivos móviles" },
                    { 41, 1, "Bocinas, audífonos, televisores, proyectores, equipos de sonido.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Audio y vídeo" },
                    { 42, 1, "Videojuegos, consolas, juegos de mesa, cartas coleccionables.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juegos y entretenimiento" },
                    { 43, 1, "Analgésicos, antihistamínicos, medicinas sin receta médica.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Medicamentos de venta libre" },
                    { 44, 1, "Multivitamínicos, proteínas, hierro, calcio, omega-3 y energía.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vitaminas y suplementos" },
                    { 45, 1, "Crema hidratante, protector solar, limpiadores faciales, exfoliantes.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos de cuidado de la piel" },
                    { 46, 1, "Balones, raquetas, pesas, ropa deportiva, calzado para ejercicio.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Equipamiento deportivo" },
                    { 47, 1, "Comida, collares, juguetes, camas, productos de higiene animal.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suministros para mascotas" },
                    { 48, 1, "Cuadernos, lápices, plumas, marcadores, material escolar y de oficina.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Papelería" },
                    { 49, 1, "Macetas, tierra, plantas, herramientas, abonos y riego.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Artículos de jardinería" },
                    { 50, 1, "Destornilladores, martillos, sierras, cinta métrica, nivel, taladros.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Herramientas para el hogar" },
                    { 51, 1, "Libros, revistas, DVDs, música, libros usados o nuevos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Libros y medios" },
                    { 52, 1, "Venta de flores naturales, arreglos florales, ramos para eventos y decoración.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Floristería" },
                    { 53, 1, "Dulces artesanales, confites, productos tradicionales y golosinas típicas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pandulce" },
                    { 54, 1, "Tienda de abarrotes con productos variados: alimentos, limpieza, bebidas y más.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Miscelánea" },
                    { 55, 1, "Venta de vidrios, espejos, marcos, cristales para ventanas y puertas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Vidriería" },
                    { 56, 1, "Materiales industriales, agrícolas o de construcción por mayor o menor.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venta de materia prima" },
                    { 57, 1, "Venta de barras, alambres, clavos, acero y otros productos metálicos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Barretería" },
                    { 58, 1, "Especializado en comidas preparadas: platos fuertes, menús diarios, comida gourmet.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurante" },
                    { 59, 1, "Helados, sorbetes, nieves, paletas y postres helados artesanales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sorbetería" },
                    { 60, 1, "Venta de insumos agrícolas: semillas, fertilizantes, plaguicidas, herramientas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agroservicio" },
                    { 61, 1, "Combustible, aceite, lubricantes, neumáticos y productos automotrices.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gasolinera" },
                    { 62, 1, "Pollos, gallinas, pavos, huevos fértiles y aves vivas o procesadas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venta de aves" },
                    { 63, 1, "Automóviles nuevos o usados, venta directa o financiada.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venta de autos" },
                    { 64, 1, "Motocicletas nuevas o usadas, accesorios y repuestos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Venta de motos" },
                    { 65, 1, "Pinturas especiales para vehículos, barnices, selladores y equipo de pintado.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pintura automotriz" },
                    { 66, 1, "Tejas artesanales de barro cocido para techos tradicionales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teja de barro" },
                    { 67, 1, "Ladrillos rojos de barro para construcción tradicional.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ladrillo de barro" },
                    { 68, 2, "Consulta, vacunación, cirugía y cuidado de animales domésticos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Veterinaria" },
                    { 69, 2, "Limpieza, extracciones, ortodoncia, implantes y salud bucal.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clínica dental" },
                    { 70, 2, "Asesoría legal, defensa penal, derecho civil, contratos y familia.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Abogado" },
                    { 71, 2, "Legalización de documentos, testamentos, poderes, escrituras públicas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Notario" },
                    { 72, 2, "Fotos de eventos, retratos, bodas, sesiones artísticas y edición.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fotografía" },
                    { 73, 2, "Instalación y reparación de tuberías, grifos, drenajes y sistemas de agua.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fontanería" },
                    { 74, 2, "Construcción, reparación de paredes, techos, pisos y estructuras en general.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Albañilería" },
                    { 75, 2, "Entrenamiento físico, clases grupales, máquinas, rutinas y nutrición.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gimnasio" },
                    { 76, 2, "Tratamientos faciales, depilación, maquillaje, manicure y cuidado corporal.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Estética" },
                    { 77, 2, "Masajes relajantes, terapias, baños termales, bienestar y relajación profunda.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spa" },
                    { 78, 2, "Examen visual, venta de lentes, monturas, lentes de contacto y accesorios.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Óptica" },
                    { 79, 2, "Cuidado infantil, actividades educativas y supervisión para niños pequeños.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guardería" },
                    { 80, 2, "Clases de inglés, francés, alemán, español para extranjeros, etc.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Academia de idiomas" },
                    { 81, 2, "Reservas de vuelos, paquetes turísticos, hoteles y tours guiados.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Agencia de viajes" },
                    { 82, 2, "Equipo de abogados especializados en distintas áreas del derecho.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bufete jurídico" },
                    { 83, 2, "Preparación y entrega de comida para eventos, bodas, reuniones.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servicio de catering" },
                    { 84, 2, "Reparación de autos, motos, cambios de aceite, frenos y diagnóstico.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taller mecánico" },
                    { 85, 2, "Instalación, mantenimiento y reparación de sistemas eléctricos residenciales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Electricista" },
                    { 86, 2, "Logotipos, tarjetas de presentación, banners, identidad visual.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diseño gráfico" },
                    { 87, 2, "Creación y mantenimiento de sitios web, tiendas online y plataformas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Desarrollo web" },
                    { 88, 2, "Asesoría en gestión, finanzas, marketing y crecimiento de negocios.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consultoría empresarial" },
                    { 89, 2, "Transporte de muebles, embalaje, carga y descarga de viviendas u oficinas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mudanzas" },
                    { 90, 2, "Limpieza de hogares, oficinas, ventanas, alfombras y desinfección.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Limpieza y mantenimiento" },
                    { 91, 2, "Reparación de neveras, lavadoras, hornos, microondas y más.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reparación de electrodomésticos" },
                    { 92, 2, "Soporte técnico, redes, configuración de dispositivos y seguridad.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tecnología y soporte IT" },
                    { 93, 2, "Taxis, delivery, mensajería, transporte de personas o mercancías.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servicio de transporte" },
                    { 94, 2, "Bodas, cumpleaños, conferencias, decoración, logística y coordinación.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Organización de eventos" },
                    { 95, 2, "Apoyo emocional, terapia individual, parejas, ansiedad, depresión.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Terapia psicológica" },
                    { 96, 2, "Mantenimiento de jardines, poda, riego, paisajismo y limpieza exterior.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Servicio de jardinería" },
                    { 97, 2, "Estética personal, peluquería, manicure, maquillaje y venta de productos de belleza.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Salón y Boutique" },
                    { 98, 2, "Servicio de comida preparada: pupusas, platillos típicos, almuerzos económicos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedor y pupusería" },
                    { 99, 2, "Asesoría legal, redacción de documentos, juicios y consultas especializadas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despacho jurídico" },
                    { 100, 2, "Equipo de abogados que ofrece servicios legales integrales en distintas áreas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bufete" },
                    { 101, 2, "Servicio de transporte privado mediante aplicación, viajes urbanos y aeropuertos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uber" },
                    { 102, 2, "Movilización de personas o carga en rutas fijas o contratadas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Transporte" },
                    { 103, 2, "Carga y descarga de mercancías, mudanzas, envíos locales o intermunicipales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Transporte y flete" },
                    { 104, 2, "Tours guiados, paseos turísticos, visitas culturales o recreativas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Excursiones" },
                    { 105, 2, "Apertura de cerraduras, duplicado de llaves, instalación y reparación de sistemas de seguridad.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cerrajería" },
                    { 106, 2, "Clases teóricas y prácticas para obtener licencia de conducir.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Escuela de manejo" },
                    { 107, 2, "Alquiler de vehículos por horas, días o semanas para uso personal o empresarial.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Renta de autos" },
                    { 108, 2, "Instalación, mantenimiento y monitoreo de sistemas de seguridad y alarmas.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alarmas" },
                    { 109, 2, "Lavado, secado e ropa planchado, servicio express o por kilo.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lavandería" },
                    { 110, 2, "Limpieza exterior e interior de vehículos, encerado, aspirado y detalles finales.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lavado de autos" },
                    { 111, 2, "Reparación y mantenimiento de neveras, congeladores, equipos de aire acondicionado.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taller de refrigerados" },
                    { 112, 2, "Diagnóstico, reparación y mantenimiento general de vehículos automotores.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taller mecánico" },
                    { 113, 2, "Servicios especializados en motores, frenos, suspensión y sistemas eléctricos.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Taller automotriz" },
                    { 114, 2, "Tapicería, balanceo, alineación, cambio de cauchos y reencauche.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reparación de llantas" },
                    { 115, 2, "Reparación móvil o en taller de vehículos, con herramientas y diagnóstico básico.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mecánico" },
                    { 116, 2, "Tratamientos faciales, corporales, depilación láser, cuidado de la piel.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cosmetología" },
                    { 117, 2, "Formación profesional en estética, uñas, cabello, maquillaje y spa.", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Academia de cosmetología" }
                });

            migrationBuilder.InsertData(
                table: "UsuarioRoles",
                columns: new[] { "RolId", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdPublicacion",
                table: "Comentarios",
                column: "IdPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdTienda",
                table: "Comentarios",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_IdUsuario",
                table: "Comentarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_ImagenesPublicacion_IdPublicacion",
                table: "ImagenesPublicacion",
                column: "IdPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdTiendaDestinatario",
                table: "Mensajes",
                column: "IdTiendaDestinatario");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdTiendaRemitente",
                table: "Mensajes",
                column: "IdTiendaRemitente");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdUsuarioDestinatario",
                table: "Mensajes",
                column: "IdUsuarioDestinatario");

            migrationBuilder.CreateIndex(
                name: "IX_Mensajes_IdUsuarioRemitente",
                table: "Mensajes",
                column: "IdUsuarioRemitente");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_IdTienda",
                table: "Publicaciones",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_Publicaciones_IdTipoPublicacion",
                table: "Publicaciones",
                column: "IdTipoPublicacion");

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_SeguidoresTiendaATienda_IdTiendaSeguida",
                table: "SeguidoresTiendaATienda",
                column: "IdTiendaSeguida");

            migrationBuilder.CreateIndex(
                name: "IX_SeguidoresUsuarioATienda_IdTienda",
                table: "SeguidoresUsuarioATienda",
                column: "IdTienda");

            migrationBuilder.CreateIndex(
                name: "IX_SeguidoresUsuarioAUsuario_IdSeguido",
                table: "SeguidoresUsuarioAUsuario",
                column: "IdSeguido");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategorias_CategoriaTiendaId",
                table: "Subcategorias",
                column: "CategoriaTiendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tiendas_IdCategoria",
                table: "Tiendas",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Tiendas_IdUsuario",
                table: "Tiendas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RolId",
                table: "UsuarioRoles",
                column: "RolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comentarios");

            migrationBuilder.DropTable(
                name: "ImagenesPublicacion");

            migrationBuilder.DropTable(
                name: "Mensajes");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "SeguidoresTiendaATienda");

            migrationBuilder.DropTable(
                name: "SeguidoresUsuarioATienda");

            migrationBuilder.DropTable(
                name: "SeguidoresUsuarioAUsuario");

            migrationBuilder.DropTable(
                name: "Subcategorias");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Publicaciones");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Tiendas");

            migrationBuilder.DropTable(
                name: "TiposPublicacion");

            migrationBuilder.DropTable(
                name: "CategoriasTienda");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
