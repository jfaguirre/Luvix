namespace LuvixApiServices.Models
{
    public class Tienda
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Logo { get; set; }
        public string? Portada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; } = "activo";

        // Navegación
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual CategoriaTienda Categoria { get; set; } = null!;
        public virtual ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public virtual ICollection<Mensaje> MensajesEnviados { get; set; } = new List<Mensaje>();
        public virtual ICollection<Mensaje> MensajesRecibidos { get; set; } = new List<Mensaje>();

        // Seguidores (Usuario → Tienda)
        public virtual ICollection<SeguidorUsuarioATienda> SeguidoresUsuarios { get; set; } = new List<SeguidorUsuarioATienda>();

        // Seguidores (Tienda → Tienda)
        public virtual ICollection<SeguidorTiendaATienda> TiendasQueSigue { get; set; } = new List<SeguidorTiendaATienda>(); // sigue a otras tiendas
        public virtual ICollection<SeguidorTiendaATienda> TiendasQueLaSiguen { get; set; } = new List<SeguidorTiendaATienda>(); // otras tiendas que la siguen
    }
}
