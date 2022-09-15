using FluentValidation;

namespace TodoApp.API.ModelValidation
{
    public class UserForAuthenticationValidation : AbstractValidator<UserForAuthentication>
    {
        public UserForAuthenticationValidation()
        {
            

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
