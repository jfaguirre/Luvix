namespace LuvixApiServices.Models.DTOs
{
    public class MostrarUsuariosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }        
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; }        
    }
}
