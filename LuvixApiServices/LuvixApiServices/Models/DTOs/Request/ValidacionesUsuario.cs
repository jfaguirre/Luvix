using FluentValidation;

namespace LuvixApiServices.Models.DTOs.Request
{
    public class ValidacionesUsuario: AbstractValidator<CrearUsuarioDTO>
    {
        public ValidacionesUsuario()
        {
            // Nombre del usuario
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener 3 caracteres como minimo.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZÀ-ÿ]+(?: [a-zA-ZÀ-ÿ]+)*$").WithMessage("El nombre solo puede contener letras y espacios válidos.");

            // Apellido del usuario 
            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MinimumLength(3).WithMessage("El apellido debe tener 3 caracteres como minimo.")
                .MaximumLength(100).WithMessage("El apellido no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZÀ-ÿ]+(?: [a-zA-ZÀ-ÿ]+)*$").WithMessage("El apellido solo puede contener letras y espacios válidos.");

            // Email del usuario
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .EmailAddress().WithMessage("Formato de email inválido.")
                .MinimumLength(3).WithMessage("El email debe tener 3 caracteres como minimo.")
                .MaximumLength(255).WithMessage("El email no puede exceder los 255 caracteres.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("El formato del correo electrónico no es válido.");

            // Contraseña del usuario
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .MaximumLength(100).WithMessage("La contraseña no puede exceder los 100 caracteres.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("La contraseña debe tener al menos una mayúscula, minúsculas, un número y un carácter especial.");

            // Foto de perfil del usuario (opcional)
            //RuleFor(x => x.FotoPerfil)
            //    .MaximumLength(2083).WithMessage("La URL de la foto de perfil no puede exceder los 2083 caracteres.")
            //    .When(x => !string.IsNullOrEmpty(x.FotoPerfil));
        }
    }
}
