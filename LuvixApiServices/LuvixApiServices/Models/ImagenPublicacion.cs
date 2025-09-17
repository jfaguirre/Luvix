namespace LuvixApiServices.Models
{
    public class ImagenPublicacion
    {
        public int Id { get; set; }
        public int IdPublicacion { get; set; }
        public string UrlImagen { get; set; } = null!;
        public int Orden { get; set; } = 1;
        public DateTime FechaSubida { get; set; }

        // Navegación
        public virtual Publicacion Publicacion { get; set; } = null!;
    }
}
