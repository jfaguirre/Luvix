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
        public virtual Tienda Tienda { get; set; } = null!;
        public virtual TipoPublicacion TipoPublicacion { get; set; } = null!;
        public virtual ICollection<ImagenPublicacion> Imagenes { get; set; } = new List<ImagenPublicacion>();
        public virtual ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
