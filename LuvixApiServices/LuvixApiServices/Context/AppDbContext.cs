using LuvixApiServices.Models;
using Microsoft.EntityFrameworkCore;

namespace LuvixApiServices.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        // Entidades
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<CategoriaTienda> CategoriasTienda { get; set; } = null!;
        public DbSet<Tienda> Tiendas { get; set; } = null!;
        public DbSet<TipoPublicacion> TiposPublicacion { get; set; } = null!;
        public DbSet<Publicacion> Publicaciones { get; set; } = null!;
        public DbSet<ImagenPublicacion> ImagenesPublicacion { get; set; } = null!;
        public DbSet<Comentario> Comentarios { get; set; } = null!;
        public DbSet<Mensaje> Mensajes { get; set; } = null!;

        // Entidades de relación (muchos a muchos)
        public DbSet<SeguidorUsuarioAUsuario> SeguidoresUsuarioAUsuario { get; set; } = null!;
        public DbSet<SeguidorUsuarioATienda> SeguidoresUsuarioATienda { get; set; } = null!;
        public DbSet<SeguidorTiendaATienda> SeguidoresTiendaATienda { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Relaciones uno a muchos ---

            // Tienda → Usuario
            modelBuilder.Entity<Tienda>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tiendas)
                .HasForeignKey(t => t.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            // Tienda → CategoriaTienda
            modelBuilder.Entity<Tienda>()
                .HasOne(t => t.Categoria)
                .WithMany(c => c.Tiendas)
                .HasForeignKey(t => t.IdCategoria);

            // Publicacion → Tienda
            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.Tienda)
                .WithMany(t => t.Publicaciones)
                .HasForeignKey(p => p.IdTienda);

            // Publicacion → TipoPublicacion
            modelBuilder.Entity<Publicacion>()
                .HasOne(p => p.TipoPublicacion)
                .WithMany(tp => tp.Publicaciones)
                .HasForeignKey(p => p.IdTipoPublicacion);

            // ImagenPublicacion → Publicacion
            modelBuilder.Entity<ImagenPublicacion>()
                .HasOne(ip => ip.Publicacion)
                .WithMany(p => p.Imagenes)
                .HasForeignKey(ip => ip.IdPublicacion);

            // Comentario → Publicacion
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Publicacion)
                .WithMany(p => p.Comentarios)
                .HasForeignKey(c => c.IdPublicacion);

            // Comentario → Usuario (opcional)
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Comentarios)
                .HasForeignKey(c => c.IdUsuario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // Comentario → Tienda (opcional)
            modelBuilder.Entity<Comentario>()
                .HasOne(c => c.Tienda)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.IdTienda)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // Mensaje → UsuarioRemitente (opcional)
            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.UsuarioRemitente)
                .WithMany(u => u.MensajesEnviados)
                .HasForeignKey(m => m.IdUsuarioRemitente)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // Mensaje → TiendaRemitente (opcional)
            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.TiendaRemitente)
                .WithMany(t => t.MensajesEnviados)
                .HasForeignKey(m => m.IdTiendaRemitente)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // Mensaje → UsuarioDestinatario (opcional)
            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.UsuarioDestinatario)
                .WithMany(u => u.MensajesRecibidos)
                .HasForeignKey(m => m.IdUsuarioDestinatario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // Mensaje → TiendaDestinatario (opcional)
            modelBuilder.Entity<Mensaje>()
                .HasOne(m => m.TiendaDestinatario)
                .WithMany(t => t.MensajesRecibidos)
                .HasForeignKey(m => m.IdTiendaDestinatario)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            // --- Relaciones muchos a muchos (con tablas join) ---

            // Seguidores Usuario → Usuario
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

            // Seguidores Usuario → Tienda
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

            // Seguidores Tienda → Tienda
            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasKey(s => new { s.IdTiendaSeguidora, s.IdTiendaSeguida });

            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasOne(s => s.TiendaSeguidora)
                .WithMany(t => t.TiendasQueSigue)
                .HasForeignKey(s => s.IdTiendaSeguidora)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado

            modelBuilder.Entity<SeguidorTiendaATienda>()
                .HasOne(s => s.TiendaSeguida)
                .WithMany(t => t.TiendasQueLaSiguen)
                .HasForeignKey(s => s.IdTiendaSeguida)
                .OnDelete(DeleteBehavior.Restrict); // ✅ Agregado
        }
    }
}
