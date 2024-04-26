using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application_Layer.Commands.ModuleCommands.CreateModule
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator()
        {
            RuleFor(x => x.ModuleDTO.CourseId)
                .NotEmpty().WithMessage("CourseId is required.")
                .Length(1, 50).WithMessage("CourseId must be between 1 and 50 characters.");

            RuleFor(x => x.ModuleDTO.ModulTitle)
                .NotEmpty().WithMessage("Module title is required.")
                .Length(1, 100).WithMessage("Module title must be between 1 and 100 characters.");

            RuleFor(x => x.ModuleDTO.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");

            RuleFor(x => x.ModuleDTO.OrderInCourse)
                .GreaterThanOrEqualTo(1).WithMessage("Order in course must be greater than or equal to 1.");

            RuleFor(x => x.ModuleDTO.ResourceURL)
                .Must(BeAValidUrl).WithMessage("Resource URL is not valid.");
        }

        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }


    }
}

