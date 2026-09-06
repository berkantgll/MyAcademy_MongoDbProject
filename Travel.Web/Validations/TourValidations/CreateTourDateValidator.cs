using FluentValidation;
using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Validations.TourValidations
{
    public class CreateTourDateValidator : AbstractValidator<CreateTourDateDto>
    {
        public CreateTourDateValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tur tarihi boş bırakılamaz.");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Kontenjan 0'dan büyük olmalıdır.");
        }
    }
}