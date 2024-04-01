using Application_Layer.Commands.RegisterNewUser;
using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(user => user.UpdatingUserInfo.Role)
               .MustBeValidRole()
               .Must(role => role == "Teacher" || role == "Student").WithMessage("Role must be either 'Teacher' or 'Student'.");
            RuleFor(user => user.UpdatingUserInfo.FirstName)
               .MustBeValidName();
            RuleFor(user => user.UpdatingUserInfo.LastName)
                .MustBeValidName();
            RuleFor(user => user.UpdatingUserInfo.Email)
                .MustBeValidEmail();
            RuleFor(user => user.UpdatingUserInfo.CurrentPassword)
                .MustBeValidPassword();
            RuleFor(user => user.UpdatingUserInfo.NewPassword)
                .MustBeValidPassword();
        }
    }
}
