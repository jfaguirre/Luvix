namespace LuvixApiServices.Models
{
    public class SeguidorTiendaATienda
    {
        public int IdTiendaSeguidora { get; set; }
        public int IdTiendaSeguida { get; set; }
        public DateTime FechaSeguimiento { get; set; }

        // Navegación
        public virtual Tienda TiendaSeguidora { get; set; } = null!;
        public virtual Tienda TiendaSeguida { get; set; } = null!;
    }
}
