
using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(50)]
        public string Apellido { get; set; } = null!;
        public string Genero { get; set; } = null!;
        [StringLength(255)]
        public string Email { get; set; } = null!;
        [StringLength(255)]
        public string Password { get; set; } = null!;
        public string? FotoPerfil { get; set; } = null;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = null!;

        // Navegación
        public ICollection<Tienda> Tiendas { get; set; } = new List<Tienda>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Mensaje> MensajesEnviados { get; set; } = new List<Mensaje>();
        public ICollection<Mensaje> MensajesRecibidos { get; set; } = new List<Mensaje>();

        // Seguidores (Usuario → Usuario)
        public ICollection<SeguidorUsuarioAUsuario> Siguiendo { get; set; } = new List<SeguidorUsuarioAUsuario>(); // usuarios que sigue
        public ICollection<SeguidorUsuarioAUsuario> Seguidores { get; set; } = new List<SeguidorUsuarioAUsuario>(); // usuarios que lo siguen

        // Seguidores (Usuario → Tienda)
        public ICollection<SeguidorUsuarioATienda> TiendasQueSigue { get; set; } = new List<SeguidorUsuarioATienda>();

        // Colección de roles (muchos-a-muchos)
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
