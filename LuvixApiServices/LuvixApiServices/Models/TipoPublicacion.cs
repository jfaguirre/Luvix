namespace LuvixApiServices.Models
{
    public class TipoPublicacion
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "post";
        public string Descripcion { get; set; } = null!;

        // Navegación
        public virtual ICollection<Publicacion> Publicaciones { get; set; } = new List<Publicacion>();
    }
}
