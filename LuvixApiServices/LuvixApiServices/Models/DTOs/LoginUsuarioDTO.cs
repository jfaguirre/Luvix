using System.ComponentModel.DataAnnotations;

namespace LuvixApiServices.Models.DTOs
{
    public class LoginUsuarioDTO
    {
        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de email inválido.")]
        [MaxLength(255, ErrorMessage = "El email no puede exceder los 255 caracteres.")]
        [MinLength(3, ErrorMessage = "El email debe tener al menos 3 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
             ErrorMessage = "El formato del correo electrónico no es válido."
             )]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MaxLength(100, ErrorMessage = "La contraseña no puede exceder los 100 caracteres.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "La contraseña debe tener al menos una mayúscula, minúsculas, un número y un carácter especial."
            )]
        public string Password { get; set; }
    }
}
