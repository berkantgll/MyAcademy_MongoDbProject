using FluentValidation;
using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Validations.TourValidations
{
    public class UpdateTourProgramValidator : AbstractValidator<UpdateTourProgramDto>
    {
        public UpdateTourProgramValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Tur programı id boş bırakılamaz.");

            RuleFor(x => x.DayNumber)
                .NotNull()
                .WithMessage("Gün numarası boş bırakılamaz.")
                .GreaterThan(0)
                .WithMessage("Gün numarası 0'dan büyük olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Program başlığı boş bırakılamaz.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Program açıklaması boş bırakılamaz.");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("Şehir boş bırakılamaz.");
        }
    }
}