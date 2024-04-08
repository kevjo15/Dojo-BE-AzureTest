using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.RegisterNewUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(user => user.NewUser.Role)
               .MustBeValidRole()
               .Must(role => role == "Teacher" || role == "Student").WithMessage("Role must be either 'Teacher' or 'Student'.");
            RuleFor(user => user.NewUser.FirstName)
                .MustBeValidName();
            RuleFor(user => user.NewUser.LastName)
                .MustBeValidName();
            RuleFor(user => user.NewUser.Email)
                .MustBeValidEmail();
            RuleFor(user => user.NewUser.Password)
               .MustBeValidPassword();
        }
    }
}
