using FluentValidation;
using Travel.Web.DTOs.CategoryDtos;


namespace Travel.Web.Validations.CategoryValidations
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori ismi boş bırakılamaz.");

            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategori adı 50 karakteri geçemez.");
        }
    }
}
