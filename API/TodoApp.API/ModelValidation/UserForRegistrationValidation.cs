using FluentValidation;
using TodoApp.Core.Models;

namespace TodoApp.API.ModelValidation
{
    public class UserForRegistrationValidation : AbstractValidator<UserForRegistration>
    {
        public UserForRegistrationValidation()
        {
            RuleFor(userRegistration => userRegistration.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username is required");

            RuleFor(userRegistration => userRegistration.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(userRegistration => userRegistration.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email is required!");

           


        }
    }
}
