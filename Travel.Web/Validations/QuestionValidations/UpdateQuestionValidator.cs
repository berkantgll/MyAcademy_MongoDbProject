using FluentValidation;
using Travel.Web.DTOs.QuestionDtos;

namespace Travel.Web.Validations.QuestionValidations
{
    public class UpdateQuestionValidator : AbstractValidator<UpdateQuestionDto>
    {
        public UpdateQuestionValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Soru id boş bırakılamaz.");

            RuleFor(x => x.AnswerText)
                .NotEmpty().WithMessage("Cevap boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Cevap 1000 karakteri geçemez.");
        }
    }
}