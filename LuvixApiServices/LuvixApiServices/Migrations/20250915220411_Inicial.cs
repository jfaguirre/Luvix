using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "IX_Tiendas_IdCategoria",
                table: "Tiendas",
                column: "IdCategoria");

            migrationBuilder.CreateIndex(
                name: "IX_Tiendas_IdUsuario",
                table: "Tiendas",
                column: "IdUsuario");
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
                name: "SeguidoresTiendaATienda");

            migrationBuilder.DropTable(
                name: "SeguidoresUsuarioATienda");

            migrationBuilder.DropTable(
                name: "SeguidoresUsuarioAUsuario");

            migrationBuilder.DropTable(
                name: "Publicaciones");

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
