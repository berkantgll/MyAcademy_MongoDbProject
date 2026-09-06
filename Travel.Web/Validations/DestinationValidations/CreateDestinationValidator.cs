using FluentValidation;
using Travel.Web.DTOs.DestinationDtos;

namespace Travel.Web.Validations.DestinationValidations
{
    public class CreateDestinationValidator : AbstractValidator<CreateDestinationDto>
    {
        public CreateDestinationValidator()
        {
            RuleFor(x => x.DestinationName)
                .NotEmpty().WithMessage("Destinasyon adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Destinasyon adı 100 karakteri geçemez.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Ülke adı 100 karakteri geçemez.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Şehir adı 100 karakteri geçemez.");
        }
    }
}