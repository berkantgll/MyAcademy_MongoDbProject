using FluentValidation;
using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Validations.ReservationValidations
{
    public class CreateReservationValidator : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.TourId).NotEmpty().WithMessage("Tur seçilmelidir.");

            RuleFor(x => x.SelectedTourDate).NotEmpty().WithMessage("Tur tarihi seçilmelidir");

            RuleFor(x => x.AdultCount).GreaterThanOrEqualTo(0).WithMessage("Yetişkin sayısı negatif olamaz.");

            RuleFor(x => x.ChildCount).GreaterThanOrEqualTo(0).WithMessage("Çocuk sayısı negatif olamaz.");

            RuleFor(x => x).Must(x => x.ChildCount + x.AdultCount > 0).WithMessage("En az bir yolcu seçilmelidir");
        }
    }
}
