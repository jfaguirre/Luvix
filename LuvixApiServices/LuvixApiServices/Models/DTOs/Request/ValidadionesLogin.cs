using FluentValidation;

namespace LuvixApiServices.Models.DTOs.Request
{
    public class ValidadionesLogin : AbstractValidator<LoginUsuarioDTO>
    {
        public ValidadionesLogin()
        {
            // Email del usuario
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Formato de email inválido.")
                .MinimumLength(3).WithMessage("El email debe tener 3 caracteres como minimo.")
                .MaximumLength(255).WithMessage("El email no puede exceder los 255 caracteres.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("El formato del correo electrónico no es válido.");

            // Contraseña del usuario
            RuleFor(x => x.Password)
             .NotEmpty().WithMessage("La contraseña es obligatoria.");
        }
    }
}
