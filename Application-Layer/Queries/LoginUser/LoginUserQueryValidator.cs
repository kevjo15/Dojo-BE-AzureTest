using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.LoginUser
{
    public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
    {
        public LoginUserQueryValidator() 
        {
            RuleFor(user => user.LoginUserDTO.Email)
                .MustBeValidEmail();
            RuleFor(user => user.LoginUserDTO.Password)
                .MustBeValidPassword();
        }
    }
}
