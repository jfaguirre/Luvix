namespace LuvixApiServices.Models
{
    public class SeguidorUsuarioAUsuario
    {
        public int IdSeguidor { get; set; }
        public int IdSeguido { get; set; }
        public DateTime FechaSeguimiento { get; set; }

        // Navegación
        public virtual Usuario Seguidor { get; set; } = null!;
        public virtual Usuario Seguido { get; set; } = null!;
    }
}
