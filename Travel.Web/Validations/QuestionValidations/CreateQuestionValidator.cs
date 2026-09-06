using FluentValidation;
using Travel.Web.DTOs.QuestionDtos;

namespace Travel.Web.Validations.QuestionValidations
{
    public class CreateQuestionValidator : AbstractValidator<CreateQuestionDto>
    {
        public CreateQuestionValidator()
        {
            RuleFor(x => x.TourId)
                .NotEmpty().WithMessage("Tur bilgisi boş bırakılamaz.");

            RuleFor(x => x.QuestionText)
                .NotEmpty().WithMessage("Soru boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Soru 1000 karakteri geçemez.");
        }
    }
}