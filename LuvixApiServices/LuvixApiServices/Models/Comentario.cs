namespace LuvixApiServices.Models
{
    public class Comentario
    {
        public int Id { get; set; }
        public int IdPublicacion { get; set; }
        public int? IdUsuario { get; set; }
        public int? IdTienda { get; set; }
        public string Contenido { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = "activo";

        // Navegación
        public virtual Publicacion Publicacion { get; set; } = null!;
        public virtual Usuario? Usuario { get; set; }
        public virtual Tienda? Tienda { get; set; }
    }
}
