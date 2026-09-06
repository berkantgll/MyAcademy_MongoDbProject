using FluentValidation;
using Travel.Web.DTOs.TourDtos;

namespace Travel.Web.Validations.TourValidations
{
    public class UpdateTourValidator : AbstractValidator<UpdateTourDto>
    {
        public UpdateTourValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Tur id boş bırakılamaz.");

            RuleFor(x => x.TourName)
                .NotEmpty().WithMessage("Tur adı boş bırakılamaz.")
                .MaximumLength(150).WithMessage("Tur adı 150 karakteri geçemez.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Tur açıklaması boş bırakılamaz.");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori seçilmelidir.");

            RuleFor(x => x.DestinationId)
                .NotEmpty().WithMessage("Destinasyon seçilmelidir.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Tur fiyatı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Tur fiyatı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Day)
                .NotNull().WithMessage("Gün sayısı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Gün sayısı 0'dan büyük olmalıdır.");

            RuleFor(x => x.Night)
                .NotNull().WithMessage("Gece sayısı boş bırakılamaz.")
                .GreaterThanOrEqualTo(0).WithMessage("Gece sayısı negatif olamaz.");
        }
    }
}