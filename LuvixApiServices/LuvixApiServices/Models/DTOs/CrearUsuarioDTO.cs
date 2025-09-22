using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models.DTOs
{
    public class CrearUsuarioDTO
    {       
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;        

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
        

    }
}
