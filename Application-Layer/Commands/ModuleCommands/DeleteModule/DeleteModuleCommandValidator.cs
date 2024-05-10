using FluentValidation;

namespace Application_Layer.Commands.ModuleCommands.DeleteModule
{
    public class DeleteModuleCommandValidator : AbstractValidator<DeleteModuleCommand>
    {
        public DeleteModuleCommandValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required.");
        }
    }
}
