using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models
{
    public class TipoPublicacion
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        public string Descripcion { get; set; } = null!;

        // Navegación
        public ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}
