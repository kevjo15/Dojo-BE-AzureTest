using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTO_s.Module;
using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.ModuleCommands.UpdateModule
{
    public class UpdateModuleValidator : AbstractValidator<UpdateModuleCommand>
    {
        public UpdateModuleValidator()
        {
            RuleFor(command => command.ModuleId)
                .MustBeValidId();

            RuleFor(command => command.ModuleUpdateDTO.CourseId)
                .MustBeValidId();

            RuleFor(command => command.ModuleUpdateDTO.ModulTitle)
                .NotEmpty().WithMessage("Module title is required.")
                .Length(1, 100).WithMessage("Module title must be between 1 and 100 characters.");

            RuleFor(command => command.ModuleUpdateDTO.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

            RuleFor(command => command.ModuleUpdateDTO.OrderInCourse)
                .GreaterThanOrEqualTo(1).WithMessage("Order in course must be greater than or equal to 1.");

            RuleFor(command => command.ModuleUpdateDTO.ResourceURL)
                .Must(BeAValidUrl).When(command => !string.IsNullOrEmpty(command.ModuleUpdateDTO.ResourceURL))
                .WithMessage("Resource URL must be a valid URL.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
