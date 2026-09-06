using FluentValidation;
using Travel.Web.DTOs.UserDtos;

namespace Travel.Web.Validations.UserValidations
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Ad soyad boş bırakılamaz.")
                .MaximumLength(100)
                .WithMessage("Ad soyad 100 karakteri geçemez.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta boş bırakılamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Telefon numarası boş bırakılamaz.")
                .MaximumLength(20)
                .WithMessage("Telefon numarası 20 karakteri geçemez.");
        }
    }
}