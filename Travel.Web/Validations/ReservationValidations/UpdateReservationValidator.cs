using FluentValidation;
using Travel.Web.DTOs.ReservationDtos;

namespace Travel.Web.Validations.ReservationValidations
{
    public class UpdateReservationValidator : AbstractValidator<UpdateReservationDto>
    {
        public UpdateReservationValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Rezervasyon id boş bırakılamaz.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Rezervasyon durumu boş bırakılamaz.")
                .Must(x =>
                    x == "Bekliyor" ||
                    x == "Onaylandı" ||
                    x == "İptal Edildi")
                .WithMessage("Geçersiz rezervasyon durumu.");
        }
    }
}