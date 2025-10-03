namespace LuvixApiServices.Models
{
    public class SeguidorUsuarioAUsuario
    {
        public int IdSeguidor { get; set; }
        public int IdSeguido { get; set; }
        public DateTime FechaSeguimiento { get; set; }

        // Navegación
        public Usuario Seguidor { get; set; } = null!;
        public Usuario Seguido { get; set; } = null!;
    }
}
