using FluentValidation;
using Travel.Web.DTOs.UserDtos;

namespace Travel.Web.Validators.UserValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.NameSurname)
                .NotEmpty()
                .WithMessage("Ad soyad boş bırakılamaz.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("E-posta boş bırakılamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre boş bırakılamaz.")
                .MinimumLength(6)
                .WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor.");
        }
    }
}