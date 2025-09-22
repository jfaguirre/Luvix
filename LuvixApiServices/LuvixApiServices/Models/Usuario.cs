
namespace LuvixApiServices.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FotoPerfil { get; set; } = null;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = "activo";

        // Navegación
        public virtual ICollection<Tienda> Tiendas { get; set; } = new List<Tienda>();
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public virtual ICollection<Mensaje> MensajesEnviados { get; set; } = new List<Mensaje>();
        public virtual ICollection<Mensaje> MensajesRecibidos { get; set; } = new List<Mensaje>();

        // Seguidores (Usuario → Usuario)
        public virtual ICollection<SeguidorUsuarioAUsuario> Siguiendo { get; set; } = new List<SeguidorUsuarioAUsuario>(); // usuarios que sigue
        public virtual ICollection<SeguidorUsuarioAUsuario> Seguidores { get; set; } = new List<SeguidorUsuarioAUsuario>(); // usuarios que lo siguen

        // Seguidores (Usuario → Tienda)
        public virtual ICollection<SeguidorUsuarioATienda> TiendasQueSigue { get; set; } = new List<SeguidorUsuarioATienda>();
    }
}
