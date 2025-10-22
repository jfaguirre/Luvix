using LuvixApiServices.CustomProperties;
using LuvixApiServices.Models;
using Microsoft.EntityFrameworkCore;

namespace LuvixApiServices.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Entidades
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<CategoriaTienda> CategoriasTienda { get; set; } = null!;
        public DbSet<Tienda> Tiendas { get; set; } = null!;
        public DbSet<TipoPublicacion> TiposPublicacion { get; set; } = null!;
        public DbSet<Publicacion> Publicaciones { get; set; } = null!;
        public DbSet<ImagenPublicacion> ImagenesPublicacion { get; set; } = null!;
        public DbSet<Comentario> Comentarios { get; set; } = null!;
        public DbSet<Mensaje> Mensajes { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<Permiso> Permisos { get; set; } = null!;
        public DbSet<UsuarioRol> UsuarioRoles { get; set; } = null!;
        public DbSet<RolPermiso> RolPermisos { get; set; } = null!;
        public DbSet<SubCategoria> Subcategorias { get; set; } = null!;

        // Relaciones muchos-a-muchos
        public DbSet<SeguidorUsuarioAUsuario> SeguidoresUsuarioAUsuario { get; set; } = null!;
        public DbSet<SeguidorUsuarioATienda> SeguidoresUsuarioATienda { get; set; } = null!;
        public DbSet<SeguidorTiendaATienda> SeguidoresTiendaATienda { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ======================
            // Relaciones Uno a Muchos
            // ======================

            modelBuilder.Entity<Tienda>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tiendas)
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tienda>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tiendas)
                .HasForeignKey(t => t.IdCategoria);

            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.Tienda)
                .WithMany(t => t.Publicaciones)
                .HasForeignKey(p => p.IdTienda);

            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.TipoPublicacion)
                .WithMany(tp => tp.Publicaciones)
                .HasForeignKey(p => p.IdTipoPublicacion);

            modelBuilder.Entity<ImagenPublicacion>()
                .HasOne(ip => ip.Publicacion)
                .WithMany(p => p.Imagenes)
                .HasForeignKey(ip => ip.IdPublicacion);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Publicacion)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.IdPublicacion);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.IdUsuario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Tienda)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.IdTienda)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.UsuarioRemitente)
                .WithMany(u => u.MensajesEnviados)
                .HasForeignKey(m => m.IdUsuarioRemitente)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.TiendaRemitente)
                .WithMany(t => t.MensajesEnviados)
                .HasForeignKey(m => m.IdTiendaRemitente)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.UsuarioDestinatario)
                .WithMany(u => u.MensajesRecibidos)
                .HasForeignKey(m => m.IdUsuarioDestinatario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.TiendaDestinatario)
                .WithMany(t => t.MensajesRecibidos)
                .HasForeignKey(m => m.IdTiendaDestinatario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SubCategoria>()
                .HasOne(s => s.CategoriaTienda)
                .WithMany(ct => ct.Subcategorias)
                .HasForeignKey(s => s.CategoriaTiendaId)
                .OnDelete(DeleteBehavior.Restrict);

            // ============================
            // Relaciones Muchos a Muchos
            // ============================

            // Usuario ↔ Rol
            modelBuilder.Entity<UsuarioRol>()
                .HasKey(ur => new { ur.UsuarioId, ur.RolId });

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Usuario)
                .WithMany(u => u.UsuarioRoles)
                .HasForeignKey(ur => ur.UsuarioId);

            modelBuilder.Entity<UsuarioRol>()
                .HasOne(ur => ur.Rol)
                .WithMany(r => r.UsuarioRoles)
                .HasForeignKey(ur => ur.RolId);

            // Rol ↔ Permiso
            modelBuilder.Entity<RolPermiso>()
                .HasKey(rp => new { rp.RolId, rp.PermisoId });

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Rol)
                .WithMany(r => r.RolPermisos)
                .HasForeignKey(rp => rp.RolId);

            modelBuilder.Entity<RolPermiso>()
                .HasOne(rp => rp.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(rp => rp.PermisoId);

            // Seguidor Usuario → Usuario
            modelBuilder.Entity<SeguidorUsuarioAUsuario>()
                .HasKey(s => new { s.IdSeguidor, s.IdSeguido });

            modelBuilder.Entity<SeguidorUsuarioAUsuario>()
                .HasOne(s => s.Seguidor)
                .WithMany(u => u.Siguiendo)
                .HasForeignKey(s => s.IdSeguidor)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeguidorUsuarioAUsuario>()
                .HasOne(s => s.Seguido)
                .WithMany(u => u.Seguidores)
                .HasForeignKey(s => s.IdSeguido)
                .OnDelete(DeleteBehavior.Restrict);

            // Seguidor Usuario → Tienda
            modelBuilder.Entity<SeguidorUsuarioATienda>()
                .HasKey(s => new { s.IdUsuario, s.IdTienda });

            modelBuilder.Entity<SeguidorUsuarioATienda>()
                .HasOne(s => s.Usuario)
                .WithMany(u => u.TiendasQueSigue)
                .HasForeignKey(s => s.IdUsuario);

            modelBuilder.Entity<SeguidorUsuarioATienda>()
                .HasOne(s => s.Tienda)
                .WithMany(t => t.SeguidoresUsuarios)
                .HasForeignKey(s => s.IdTienda);

            // Seguidor Tienda → Tienda
            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasKey(s => new { s.IdTiendaSeguidora, s.IdTiendaSeguida });

            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasOne(s => s.TiendaSeguidora)
                .WithMany(t => t.TiendasQueSigue)
                .HasForeignKey(s => s.IdTiendaSeguidora)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasOne(s => s.TiendaSeguida)
                .WithMany(t => t.TiendasQueLaSiguen)
                .HasForeignKey(s => s.IdTiendaSeguida)
                .OnDelete(DeleteBehavior.Restrict);

            // Poblacion de la base de datos

            // Crear usuarios
            // Crear usuarios admin
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Juan Francisco",
                    Apellido = "Aguirre Aparicio",
                    Genero = "Masculino",
                    Email = "jfaguirrex@outlook.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 2,
                    Nombre = "Margarita Elizabeth",
                    Apellido = "Santana Martínez",
                    Genero = "Femenino",
                    Email = "mesantana@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 3,
                    Nombre = "Andrea Yamileth",
                    Apellido = "Castellanos Sánchez",
                    Genero = "Femenino",
                    Email = "aycastellanos@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 4,
                    Nombre = "Alisson Stefany",
                    Apellido = "García Melgar",
                    Genero = "Femenino",
                    Email = "asgarcia@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 5,
                    Nombre = "Fátima Roció",
                    Apellido = "Guerrero Mena",
                    Genero = "Femenino",
                    Email = "frguerrero@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 6,
                    Nombre = "Rodrigo Yohalmo",
                    Apellido = "Echegoyen Henríquez",
                    Genero = "Masculino",
                    Email = "ryechegoyen@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                },

                new Usuario
                {
                    Id = 7,
                    Nombre = "Oscar Daniel",
                    Apellido = "López Ramírez",
                    Genero = "Masculino",
                    Email = "odlopez@gmail.com",
                    Password = "Admin2025@",
                    FotoPerfil = null,
                    FechaRegistro = new DateTime(2025, 10, 1),
                    Estado = "activo"
                }
            );


            // Roles
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Admin" },
                new Rol { Id = 2, Nombre = "Editor" },
                new Rol { Id = 3, Nombre = "Vendedor" }
            );

            // Permisos
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { Id = 1, Nombre = "EditarUsuario", Descripcion = "Puede editar usuario" },
                new Permiso { Id = 2, Nombre = "EliminarUsuario", Descripcion = "Puede eliminar usuarios" },
                new Permiso { Id = 3, Nombre = "BloquearUsuario", Descripcion = "Puede bloquear usuarios" },
                new Permiso { Id = 4, Nombre = "DesbloquearUsuario", Descripcion = "Puede desbloquear usuarios" }
            );

            // Asignación de permisos al rol Admin
            modelBuilder.Entity<RolPermiso>().HasData(
                new RolPermiso { RolId = 1, PermisoId = 1 },
                new RolPermiso { RolId = 1, PermisoId = 2 },
                new RolPermiso { RolId = 1, PermisoId = 3 },
                new RolPermiso { RolId = 1, PermisoId = 4 }
            );

            // Asignar rol a un usuario
            modelBuilder.Entity<UsuarioRol>().HasData(
                new UsuarioRol { UsuarioId = 1, RolId = 1 },
                new UsuarioRol { UsuarioId = 2, RolId = 1 },
                new UsuarioRol { UsuarioId = 3, RolId = 1 },
                new UsuarioRol { UsuarioId = 4, RolId = 1 },
                new UsuarioRol { UsuarioId = 5, RolId = 1 },
                new UsuarioRol { UsuarioId = 6, RolId = 1 },
                new UsuarioRol { UsuarioId = 7, RolId = 1 }
            );

            // Categorías Tienda
            modelBuilder.Entity<CategoriaTienda>().HasData(
                new CategoriaTienda { Id = 1, Nombre = "Producto" },
                new CategoriaTienda { Id = 2, Nombre = "Servicio" }
            );

            // Subcategorias
            modelBuilder.Entity<SubCategoria>().HasData(
                // Productos (CategoriaTiendaId = 1)  
                new { Id = 1, Nombre = "Productos frescos", Descripcion = "Frutas (manzanas, bananas, naranjas), verduras (lechuga, tomates, papas), carne, pescado.", FechaCreacion = new DateTime(2025,10,1), CategoriaTiendaId = 1 },
                new { Id = 2, Nombre = "Pastelería", Descripcion = "Tortas, pasteles, cupcakes, postres decorados y repostería artesanal.", FechaCreacion = new DateTime(2025,10,1), CategoriaTiendaId = 1 },
                new { Id = 3, Nombre = "Bisutería", Descripcion = "Collares, pulseras, aretes, anillos y accesorios decorativos asequibles.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 4, Nombre = "Ferretería", Descripcion = "Herramientas, clavos, tornillos, pinturas, materiales eléctricos y construcción.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 5, Nombre = "Farmacia", Descripcion = "Medicamentos, productos de higiene, primeros auxilios y artículos médicos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 6, Nombre = "Ropa", Descripcion = "Prendas para hombres, mujeres y niños: camisas, pantalones, vestidos, ropa interior.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 7, Nombre = "Calzado", Descripcion = "Zapatos, botas, sandalias, tenis y calzado deportivo o formal.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 8, Nombre = "Artesanías", Descripcion = "Objetos hechos a mano: cerámica, madera, tejidos, decoración cultural.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 9, Nombre = "Alimentos típicos", Descripcion = "Comidas tradicionales locales: tamales, pupusas, atoles, platos regionales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 10, Nombre = "Productos lácteos", Descripcion = "Leche, queso, yogur, mantequilla, crema y derivados lácteos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 11, Nombre = "Productos tecnológicos", Descripcion = "Dispositivos electrónicos como tablets, cámaras, drones y gadgets.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 12, Nombre = "Tienda mayorista", Descripcion = "Venta de productos en grandes cantidades a bajo costo para revender.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 13, Nombre = "Tienda minorista", Descripcion = "Venta directa al consumidor final de productos variados.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 14, Nombre = "Pupusería", Descripcion = "Especializada en la venta de pupusas rellenas con queso, frijol, chicharrón, etc.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 15, Nombre = "Juguetería", Descripcion = "Juguetes para niños: muñecos, juegos educativos, rompecabezas, vehículos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 16, Nombre = "Textil", Descripcion = "Telas, hilos, telares, productos para confección y diseño de ropa.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 17, Nombre = "Muebles", Descripcion = "Sillas, mesas, camas, closets, muebles para hogar u oficina.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 18, Nombre = "Cervecería", Descripcion = "Cerveza artesanal, barriles, botellas, marcas locales e importadas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 19, Nombre = "Estructuras metálicas", Descripcion = "Puertas, rejas, escaleras, techos y estructuras de acero o aluminio.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 20, Nombre = "Productos de la canasta básica", Descripcion = "Arroz, azúcar, frijoles, aceite, sal, fósforos, productos esenciales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 21, Nombre = "Relojería", Descripcion = "Relojes de pulsera, de pared, cronómetros y accesorios de tiempo.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 22, Nombre = "Tienda", Descripcion = "Establecimiento general que vende una variedad de productos de consumo diario.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 23, Nombre = "Pizzería", Descripcion = "Pizzas al horno, porciones, familiares, con diferentes ingredientes y sabores.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 24, Nombre = "Impresoras", Descripcion = "Impresoras láser, inalámbricas, multifuncionales y suministros (tinta, papel).", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 25, Nombre = "Computadoras", Descripcion = "Laptops, PCs, componentes, monitores, teclados y periféricos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 26, Nombre = "Taquería", Descripcion = "Tacos, burritos, quesadillas, tortillas frescas y salsas caseras.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 27, Nombre = "Cafetería", Descripcion = "Café, capuchino, expresso, pastelillos, jugos naturales y snacks.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 28, Nombre = "Depósito de telas", Descripcion = "Venta al por mayor de telas para confección, moda o tapicería.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 29, Nombre = "Depósito de bebidas", Descripcion = "Refrescos, jugos, agua embotellada, cerveza, energizantes en volumen.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 30, Nombre = "Supermercado", Descripcion = "Gran establecimiento que ofrece alimentos, limpieza, ropa y más.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 31, Nombre = "Comida rápida", Descripcion = "Hamburguesas, hot dogs, papas fritas, pollo frito, comida para llevar.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 32, Nombre = "Chocolate artesanal", Descripcion = "Chocolates finos, bombones, trufas, barras y dulces hechos a mano.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 33, Nombre = "Productos avícolas", Descripcion = "Huevos, pollo, gallinas, pavos y subproductos avícolas frescos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 34, Nombre = "Higiene y cuidado personal", Descripcion = "Jabón, champú, cepillo dental, toallas higiénicas, desodorante.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 35, Nombre = "Panadería", Descripcion = "Pan, bollos, pan dulce, pan de molde, baguettes y productos horneados.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 36, Nombre = "Electrodomésticos", Descripcion = "Refrigeradores, estufas, licuadoras, freidoras, lavadoras, microondas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 37, Nombre = "Artículos de cocina", Descripcion = "Utensilios, ollas, sartenes, platos, cubiertos y herramientas de cocina.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 38, Nombre = "Muebles y decoración", Descripcion = "Decoración interior, lámparas, cuadros, alfombras, muebles pequeños.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 39, Nombre = "Accesorios", Descripcion = "Gorras, bolsos, cinturones, lentes de sol, bufandas y llaveros.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 40, Nombre = "Dispositivos móviles", Descripcion = "Celulares, smartphones, accesorios, fundas, cargadores, audífonos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 41, Nombre = "Audio y vídeo", Descripcion = "Bocinas, audífonos, televisores, proyectores, equipos de sonido.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 42, Nombre = "Juegos y entretenimiento", Descripcion = "Videojuegos, consolas, juegos de mesa, cartas coleccionables.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 43, Nombre = "Medicamentos de venta libre", Descripcion = "Analgésicos, antihistamínicos, medicinas sin receta médica.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 44, Nombre = "Vitaminas y suplementos", Descripcion = "Multivitamínicos, proteínas, hierro, calcio, omega-3 y energía.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 45, Nombre = "Productos de cuidado de la piel", Descripcion = "Crema hidratante, protector solar, limpiadores faciales, exfoliantes.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 46, Nombre = "Equipamiento deportivo", Descripcion = "Balones, raquetas, pesas, ropa deportiva, calzado para ejercicio.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 47, Nombre = "Suministros para mascotas", Descripcion = "Comida, collares, juguetes, camas, productos de higiene animal.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 48, Nombre = "Papelería", Descripcion = "Cuadernos, lápices, plumas, marcadores, material escolar y de oficina.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 49, Nombre = "Artículos de jardinería", Descripcion = "Macetas, tierra, plantas, herramientas, abonos y riego.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 50, Nombre = "Herramientas para el hogar", Descripcion = "Destornilladores, martillos, sierras, cinta métrica, nivel, taladros.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 51, Nombre = "Libros y medios", Descripcion = "Libros, revistas, DVDs, música, libros usados o nuevos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 52, Nombre = "Floristería", Descripcion = "Venta de flores naturales, arreglos florales, ramos para eventos y decoración.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 53, Nombre = "Pandulce", Descripcion = "Dulces artesanales, confites, productos tradicionales y golosinas típicas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 54, Nombre = "Miscelánea", Descripcion = "Tienda de abarrotes con productos variados: alimentos, limpieza, bebidas y más.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 55, Nombre = "Vidriería", Descripcion = "Venta de vidrios, espejos, marcos, cristales para ventanas y puertas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 56, Nombre = "Venta de materia prima", Descripcion = "Materiales industriales, agrícolas o de construcción por mayor o menor.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 57, Nombre = "Barretería", Descripcion = "Venta de barras, alambres, clavos, acero y otros productos metálicos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 58, Nombre = "Restaurante", Descripcion = "Especializado en comidas preparadas: platos fuertes, menús diarios, comida gourmet.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 59, Nombre = "Sorbetería", Descripcion = "Helados, sorbetes, nieves, paletas y postres helados artesanales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 60, Nombre = "Agroservicio", Descripcion = "Venta de insumos agrícolas: semillas, fertilizantes, plaguicidas, herramientas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 61, Nombre = "Gasolinera", Descripcion = "Combustible, aceite, lubricantes, neumáticos y productos automotrices.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 62, Nombre = "Venta de aves", Descripcion = "Pollos, gallinas, pavos, huevos fértiles y aves vivas o procesadas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 63, Nombre = "Venta de autos", Descripcion = "Automóviles nuevos o usados, venta directa o financiada.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 64, Nombre = "Venta de motos", Descripcion = "Motocicletas nuevas o usadas, accesorios y repuestos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 65, Nombre = "Pintura automotriz", Descripcion = "Pinturas especiales para vehículos, barnices, selladores y equipo de pintado.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 66, Nombre = "Teja de barro", Descripcion = "Tejas artesanales de barro cocido para techos tradicionales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },
                new { Id = 67, Nombre = "Ladrillo de barro", Descripcion = "Ladrillos rojos de barro para construcción tradicional.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 1 },

            //// Servicios (CategoriaTiendaId = 2)
                new { Id = 68, Nombre = "Veterinaria", Descripcion = "Consulta, vacunación, cirugía y cuidado de animales domésticos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 69, Nombre = "Clínica dental", Descripcion = "Limpieza, extracciones, ortodoncia, implantes y salud bucal.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 70, Nombre = "Abogado", Descripcion = "Asesoría legal, defensa penal, derecho civil, contratos y familia.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 71, Nombre = "Notario", Descripcion = "Legalización de documentos, testamentos, poderes, escrituras públicas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 72, Nombre = "Fotografía", Descripcion = "Fotos de eventos, retratos, bodas, sesiones artísticas y edición.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 73, Nombre = "Fontanería", Descripcion = "Instalación y reparación de tuberías, grifos, drenajes y sistemas de agua.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 74, Nombre = "Albañilería", Descripcion = "Construcción, reparación de paredes, techos, pisos y estructuras en general.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 75, Nombre = "Gimnasio", Descripcion = "Entrenamiento físico, clases grupales, máquinas, rutinas y nutrición.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 76, Nombre = "Estética", Descripcion = "Tratamientos faciales, depilación, maquillaje, manicure y cuidado corporal.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 77, Nombre = "Spa", Descripcion = "Masajes relajantes, terapias, baños termales, bienestar y relajación profunda.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 78, Nombre = "Óptica", Descripcion = "Examen visual, venta de lentes, monturas, lentes de contacto y accesorios.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 79, Nombre = "Guardería", Descripcion = "Cuidado infantil, actividades educativas y supervisión para niños pequeños.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 80, Nombre = "Academia de idiomas", Descripcion = "Clases de inglés, francés, alemán, español para extranjeros, etc.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 81, Nombre = "Agencia de viajes", Descripcion = "Reservas de vuelos, paquetes turísticos, hoteles y tours guiados.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 82, Nombre = "Bufete jurídico", Descripcion = "Equipo de abogados especializados en distintas áreas del derecho.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 83, Nombre = "Servicio de catering", Descripcion = "Preparación y entrega de comida para eventos, bodas, reuniones.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 84, Nombre = "Taller mecánico", Descripcion = "Reparación de autos, motos, cambios de aceite, frenos y diagnóstico.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 85, Nombre = "Electricista", Descripcion = "Instalación, mantenimiento y reparación de sistemas eléctricos residenciales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 86, Nombre = "Diseño gráfico", Descripcion = "Logotipos, tarjetas de presentación, banners, identidad visual.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 87, Nombre = "Desarrollo web", Descripcion = "Creación y mantenimiento de sitios web, tiendas online y plataformas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 88, Nombre = "Consultoría empresarial", Descripcion = "Asesoría en gestión, finanzas, marketing y crecimiento de negocios.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 89, Nombre = "Mudanzas", Descripcion = "Transporte de muebles, embalaje, carga y descarga de viviendas u oficinas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 90, Nombre = "Limpieza y mantenimiento", Descripcion = "Limpieza de hogares, oficinas, ventanas, alfombras y desinfección.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 91, Nombre = "Reparación de electrodomésticos", Descripcion = "Reparación de neveras, lavadoras, hornos, microondas y más.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 92, Nombre = "Tecnología y soporte IT", Descripcion = "Soporte técnico, redes, configuración de dispositivos y seguridad.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 93, Nombre = "Servicio de transporte", Descripcion = "Taxis, delivery, mensajería, transporte de personas o mercancías.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 94, Nombre = "Organización de eventos", Descripcion = "Bodas, cumpleaños, conferencias, decoración, logística y coordinación.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 95, Nombre = "Terapia psicológica", Descripcion = "Apoyo emocional, terapia individual, parejas, ansiedad, depresión.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 96, Nombre = "Servicio de jardinería", Descripcion = "Mantenimiento de jardines, poda, riego, paisajismo y limpieza exterior.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 97, Nombre = "Salón y Boutique", Descripcion = "Estética personal, peluquería, manicure, maquillaje y venta de productos de belleza.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 98, Nombre = "Comedor y pupusería", Descripcion = "Servicio de comida preparada: pupusas, platillos típicos, almuerzos económicos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 99, Nombre = "Despacho jurídico", Descripcion = "Asesoría legal, redacción de documentos, juicios y consultas especializadas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 100, Nombre = "Bufete", Descripcion = "Equipo de abogados que ofrece servicios legales integrales en distintas áreas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 101, Nombre = "Uber", Descripcion = "Servicio de transporte privado mediante aplicación, viajes urbanos y aeropuertos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 102, Nombre = "Transporte", Descripcion = "Movilización de personas o carga en rutas fijas o contratadas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 103, Nombre = "Transporte y flete", Descripcion = "Carga y descarga de mercancías, mudanzas, envíos locales o intermunicipales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 104, Nombre = "Excursiones", Descripcion = "Tours guiados, paseos turísticos, visitas culturales o recreativas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 105, Nombre = "Cerrajería", Descripcion = "Apertura de cerraduras, duplicado de llaves, instalación y reparación de sistemas de seguridad.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 106, Nombre = "Escuela de manejo", Descripcion = "Clases teóricas y prácticas para obtener licencia de conducir.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 107, Nombre = "Renta de autos", Descripcion = "Alquiler de vehículos por horas, días o semanas para uso personal o empresarial.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 108, Nombre = "Alarmas", Descripcion = "Instalación, mantenimiento y monitoreo de sistemas de seguridad y alarmas.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 109, Nombre = "Lavandería", Descripcion = "Lavado, secado e ropa planchado, servicio express o por kilo.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 110, Nombre = "Lavado de autos", Descripcion = "Limpieza exterior e interior de vehículos, encerado, aspirado y detalles finales.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 111, Nombre = "Taller de refrigerados", Descripcion = "Reparación y mantenimiento de neveras, congeladores, equipos de aire acondicionado.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 112, Nombre = "Taller mecánico", Descripcion = "Diagnóstico, reparación y mantenimiento general de vehículos automotores.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 113, Nombre = "Taller automotriz", Descripcion = "Servicios especializados en motores, frenos, suspensión y sistemas eléctricos.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 114, Nombre = "Reparación de llantas", Descripcion = "Tapicería, balanceo, alineación, cambio de cauchos y reencauche.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 115, Nombre = "Mecánico", Descripcion = "Reparación móvil o en taller de vehículos, con herramientas y diagnóstico básico.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 116, Nombre = "Cosmetología", Descripcion = "Tratamientos faciales, corporales, depilación láser, cuidado de la piel.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 },
                new { Id = 117, Nombre = "Academia de cosmetología", Descripcion = "Formación profesional en estética, uñas, cabello, maquillaje y spa.", FechaCreacion = new DateTime(2025, 10, 1), CategoriaTiendaId = 2 }
            );
        }
    }
}
