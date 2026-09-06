using FluentValidation;
using Travel.Web.DTOs.FavoriteDtos;

namespace Travel.Web.Validations.FavoriteValidations
{
    public class CreateFavoriteValidator : AbstractValidator<CreateFavoriteDto>
    {
        public CreateFavoriteValidator()
        {
            RuleFor(x => x.TourId)
                .NotEmpty()
                .WithMessage("Tur bilgisi boş bırakılamaz.");
        }
    }
}