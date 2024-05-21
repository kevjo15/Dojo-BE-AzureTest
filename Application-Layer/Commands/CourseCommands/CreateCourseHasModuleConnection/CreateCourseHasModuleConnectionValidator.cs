using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection
{
    public class CreateCourseHasModuleConnectionValidator : AbstractValidator<CreateCourseHasModuleConnectionCommand>
    {
        public CreateCourseHasModuleConnectionValidator()
        {
            RuleFor(c => c.CourseId).MustBeValidId();
            RuleFor(m => m.ModuleId).MustBeValidId();
        }
    }
}
