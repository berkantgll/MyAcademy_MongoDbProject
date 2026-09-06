using FluentValidation;
using Travel.Web.DTOs.CommentDtos;

namespace Travel.Web.Validations.CommentValidations
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.TourId)
                .NotEmpty().WithMessage("Tur bilgisi boş bırakılamaz.");

            RuleFor(x => x.CommentText)
                .NotEmpty().WithMessage("Yorum boş bırakılamaz.")
                .MaximumLength(1000).WithMessage("Yorum 1000 karakteri geçemez.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5)
                .WithMessage("Puan 1 ile 5 arasında olmalıdır.");
        }
    }
}