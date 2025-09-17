namespace LuvixApiServices.Models
{
    public class Mensaje
    {
        public int Id { get; set; }
        public int? IdUsuarioRemitente { get; set; }
        public int? IdTiendaRemitente { get; set; }
        public int? IdUsuarioDestinatario { get; set; }
        public int? IdTiendaDestinatario { get; set; }
        public string MensajeTexto { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
        public bool Leido { get; set; }

        // Navegación
        public virtual Usuario? UsuarioRemitente { get; set; }
        public virtual Tienda? TiendaRemitente { get; set; }
        public virtual Usuario? UsuarioDestinatario { get; set; }
        public virtual Tienda? TiendaDestinatario { get; set; }
    }
}
