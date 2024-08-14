using FluentValidation;
using ProductManagement.Queries;

namespace ProductManagement.Validators
{
    public class LoginValidator : AbstractValidator<LoginQuery>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("User name must not be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password must not be empty");
        }
    }
}
