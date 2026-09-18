using FluentValidation;
using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Validators.ReservationValidators
{
    public class CreateReservationValidator
        : AbstractValidator<CreateReservationDto>
    {
        public CreateReservationValidator()
        {
            RuleFor(x => x.TourId)
                .NotEmpty();

            RuleFor(x => x.TourDateId)
                .NotEmpty();

            RuleFor(x => x.AdultCount)
                .GreaterThan(0);

            RuleFor(x => x.ChildCount)
                .GreaterThanOrEqualTo(0);
        }
    }
}