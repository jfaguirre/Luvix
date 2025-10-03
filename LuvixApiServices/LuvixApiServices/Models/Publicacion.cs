namespace LuvixApiServices.Models
{
    public class Publicacion
    {
        public int Id { get; set; }
        public int IdTienda { get; set; }
        public int IdTipoPublicacion { get; set; }
        public string Titulo { get; set; } = null!;
        public string Contenido { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; } = "activo";

        // Navegación
        public Tienda Tienda { get; set; } = null!;
        public TipoPublicacion TipoPublicacion { get; set; } = null!;
        public ICollection<ImagenPublicacion> Imagenes { get; set; } = new List<ImagenPublicacion>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
