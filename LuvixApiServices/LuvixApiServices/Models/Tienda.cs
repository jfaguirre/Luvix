using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class Tienda
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        public string? Descripcion { get; set; }        
        public string? Logo { get; set; }        
        public string? Portada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = null!;

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public CategoriaTienda Categoria { get; set; } = null!;
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public ICollection<Mensaje> MensajesEnviados { get; set; } = new List<Mensaje>();
        public ICollection<Mensaje> MensajesRecibidos { get; set; } = new List<Mensaje>();

        // Seguidores (Usuario → Tienda)
        public ICollection<SeguidorUsuarioATienda> SeguidoresUsuarios { get; set; } = new List<SeguidorUsuarioATienda>();

        // Seguidores (Tienda → Tienda)
        public ICollection<SeguidorTiendaATienda> TiendasQueSigue { get; set; } = new List<SeguidorTiendaATienda>(); // sigue a otras tiendas
        public ICollection<SeguidorTiendaATienda> TiendasQueLaSiguen { get; set; } = new List<SeguidorTiendaATienda>(); // otras tiendas que la siguen
    }
}
