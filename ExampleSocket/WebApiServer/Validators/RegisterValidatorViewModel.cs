using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebApiServer.Data.Entities.Identity;
using WebApiServer.Models;

namespace WebApiServer.Validators
{
    public class RegisterValidatorViewModel : AbstractValidator<RegisterViewModel>
    {
        private readonly UserManager<AppUser> _userManager;
        public RegisterValidatorViewModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Поле пошта є обов'язковим!")
               .EmailAddress().WithMessage("Пошта є не коректною!")
               .DependentRules(() =>
               {
                   RuleFor(x => x.Email).Must(BeUniqueEmail)

                    .WithMessage("Дана пошта уже зареєстрована!");
               });
            RuleFor(x => x.Password)
                .NotEmpty().WithName("Password").WithMessage("Поле пароль є обов'язковим!")
                .MinimumLength(5).WithName("Password").WithMessage("Поле пароль має містити міннімум 5 символів!");

            //.Matches("[A-Z]").WithName("Password").WithMessage("Password must contain one or more capital letters.")
            //.Matches("[a-z]").WithName("Password").WithMessage("Password must contain one or more lowercase letters.")
            //.Matches(@"\d").WithName("Password").WithMessage("Password must contain one or more digits.")
            //.Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithName("Password").WithMessage("Password must contain one or more special characters.")
            //.Matches("^[^£# “”]*$").WithName("Password").WithMessage("Password must not contain the following characters £ # “” or spaces.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Поле ім'я є обов'язковим!")
                .MinimumLength(2).WithMessage("Поле має мати міннімум 2 символів!");

            RuleFor(x => x.SecondName)
               .NotEmpty().WithMessage("Поле прізвище є обов'язковим!")
               .MinimumLength(2).WithMessage("Поле має мати міннімум 2 символів!");

            RuleFor(x => x.Phone)
               .NotEmpty().WithMessage("Поле телефон є обов'язковим!")
               .MinimumLength(2).WithMessage("Поле має мати міннімум 2 символів!");

            RuleFor(x => x.Photo)
               .NotEmpty().WithMessage("Поле фото є обов'язковим!");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithName("ConfirmPassword").WithMessage("Поле є обов'язковим!")
                 .Equal(x => x.Password).WithMessage("Поролі не співпадають!");
        }
        private bool BeUniqueEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result == null;
        }
    }
}
