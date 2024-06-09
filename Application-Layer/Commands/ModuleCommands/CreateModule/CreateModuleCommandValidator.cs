using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.ModuleCommands.CreateModule
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {

            RuleFor(x => x.ModuleDTO.ModulTitle)
               .MustBeValidTitle();
            RuleFor(x => x.ModuleDTO.Description)
                .MustBeValidDescription();
            RuleFor(x => x.ModuleDTO.ResourceURL)
                .NotEmpty().When(x => !string.IsNullOrEmpty(x.ModuleDTO.ResourceURL))
                .MustBeValidURL().When(x => !string.IsNullOrEmpty(x.ModuleDTO.ResourceURL));
            RuleFor(x => x.ModuleDTO.OrderInCourse)
                .GreaterThanOrEqualTo(1).WithMessage("Order in course must be greater than or equal to 1.");
        }
    }
}

