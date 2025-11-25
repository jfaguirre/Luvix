namespace LuvixApiServices.Models.DTOs
{
    public class MostrarTiendaPublicaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Logo { get; set; } // Solo el logo será mostrado
        public bool EstaSiguiendo { get; set; }
    }
}
