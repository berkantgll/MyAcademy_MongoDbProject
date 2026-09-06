using FluentValidation;
using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Validations.TourValidations
{
    public class CreateTourProgramValidator : AbstractValidator<CreateTourProgramDto>
    {
        public CreateTourProgramValidator()
        {
            RuleFor(x => x.DayNumber)
                .GreaterThan(0).WithMessage("Gün numarası 0'dan büyük olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Program başlığı boş bırakılamaz.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Program açıklaması boş bırakılamaz.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir boş bırakılamaz.");
        }
    }
}