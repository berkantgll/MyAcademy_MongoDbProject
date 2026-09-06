using FluentValidation;
using Travel.Web.DTOs.DestinationDtos;

namespace Travel.Web.Validations.DestinationValidations
{
    public class UpdateDestinationValidator : AbstractValidator<UpdateDestinationDto>
    {
        public UpdateDestinationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Destinasyon id boş bırakılamaz.");

            RuleFor(x => x.DestinationName)
                .NotEmpty().WithMessage("Destinasyon adı boş bırakılamaz.")
                .MaximumLength(100).WithMessage("Destinasyon adı 100 karakteri geçemez.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke boş bırakılamaz.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş bırakılamaz.");
        }
    }
}