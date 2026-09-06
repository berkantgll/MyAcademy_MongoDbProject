using FluentValidation;
using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Validations.TourValidations
{
    public class UpdateTourDateValidator : AbstractValidator<UpdateTourDateDto>
    {
        public UpdateTourDateValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Tur tarihi id boş bırakılamaz.");

            RuleFor(x => x.Date)
                .NotNull().WithMessage("Tur tarihi boş bırakılamaz.");

            RuleFor(x => x.Capacity)
                .NotNull().WithMessage("Kontenjan boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Kontenjan 0'dan büyük olmalıdır.");
        }
    }
}