namespace LuvixApiServices.Models
{
    public class SeguidorUsuarioATienda
    {
        public int IdUsuario { get; set; }
        public int IdTienda { get; set; }
        public DateTime FechaSeguimiento { get; set; }

        // Navegación
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Tienda Tienda { get; set; } = null!;
    }
}
