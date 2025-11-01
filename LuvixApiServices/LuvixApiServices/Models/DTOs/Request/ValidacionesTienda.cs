using FluentValidation;

namespace LuvixApiServices.Models.DTOs.Request
{
    public class ValidacionesTienda : AbstractValidator<CrearTiendaDTO>
    {
        public ValidacionesTienda()
        {
            // Nombre de la tienda
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MinimumLength(3).WithMessage("El nombre debe tener 3 caracteres como minimo.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .Matches(@"^[a-zA-ZÀ-ÿ]+(?: [a-zA-ZÀ-ÿ]+)*$").WithMessage("El nombre solo puede contener letras y espacios válidos.");

            // Descripción de la tienda
            RuleFor(x => x.Descripcion)
                .MaximumLength(200).WithMessage("La descripción no puede exceder los 200 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Descripcion));
        }
    }
}
