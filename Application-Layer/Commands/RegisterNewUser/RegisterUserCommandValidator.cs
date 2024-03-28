using FluentValidation;

namespace Application_Layer.Commands.RegisterNewUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator() 
        {
            RuleFor(user => user.NewUser.Role)
               .NotEmpty().WithMessage("Role is required.")
               .Must(role => role == "Teacher" || role == "Student").WithMessage("Role must be either 'Teacher' or 'Student'.");
            RuleFor(user => user.NewUser.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
                .MaximumLength(20).WithMessage("First name cannot exceed 20 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.");
            RuleFor(user => user.NewUser.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
                .MaximumLength(20).WithMessage("Last name cannot exceed 20 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters.");
            RuleFor(user => user.NewUser.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid email address is required.");
            RuleFor(user => user.NewUser.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.")
                .NotEqual("password", StringComparer.OrdinalIgnoreCase)
                .WithMessage("Password cannot be 'password'.");
        }
    }
}
