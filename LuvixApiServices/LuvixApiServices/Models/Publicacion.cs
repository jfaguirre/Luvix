using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class Publicacion
    {
        public int Id { get; set; }
        public int IdTienda { get; set; }
        public int IdTipoPublicacion { get; set; }
        [StringLength(100)]
        public string Titulo { get; set; } = null!;
        [StringLength(1000)]
        public string Contenido { get; set; } = null!;
        public DateTime FechaPublicacion { get; set; }
        public string Estado { get; set; } = null!;

        // Navegación
        public Tienda Tienda { get; set; } = null!;
        public TipoPublicacion TipoPublicacion { get; set; } = null!;
        public ICollection<ImagenPublicacion> Imagenes { get; set; } = new List<ImagenPublicacion>();
        public ICollection<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
