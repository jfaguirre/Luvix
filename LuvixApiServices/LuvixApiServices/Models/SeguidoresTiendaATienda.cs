namespace LuvixApiServices.Models
{
    public class SeguidorTiendaATienda
    {
        public int IdTiendaSeguidora { get; set; }
        public int IdTiendaSeguida { get; set; }
        public DateTime FechaSeguimiento { get; set; }

        // Navegación
        public Tienda TiendaSeguidora { get; set; } = null!;
        public Tienda TiendaSeguida { get; set; } = null!;
    }
}
