using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models.DTOs
{
    public class LoginUsuarioDTO
    {    
        public string Email { get; set; }
     
        public string Password { get; set; }
    }
}
