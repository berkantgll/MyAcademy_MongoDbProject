using FluentValidation;
using Travel.Web.DTOs.CategoryDtos;

namespace Travel.Web.Validations.CategoryValidations
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage("Kategori id boş bırakılamaz.");

            RuleFor(x => x.CategoryName).NotEmpty().WithMessage("Kategori ismi boş bırakılamaz.");

            RuleFor(x => x.CategoryName).MaximumLength(50).WithMessage("Kategori adı 50 karakteri geçemez.");

        }
    }
}
