namespace LuvixApiServices.Models
{
    public class CategoriaTienda
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }

        // Navegación
        public virtual ICollection<Tienda> Tiendas { get; set; } = new List<Tienda>();
    }
}
