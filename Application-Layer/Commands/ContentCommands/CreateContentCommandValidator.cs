using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Commands.ContentCommands
{
    public class CreateContentCommandValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentCommandValidator()
        {
            RuleFor(x => x.ContentDTO.ContentTitle)
                .MustBeValidTitle();
            RuleFor(x => x.ContentDTO.Description)
                .MustBeValidDescription();
            RuleFor(x => x.ContentDTO.ContentURL)
                .NotEmpty()
                .MustBeValidURL();
            RuleFor(x => x.ContentDTO.ContentType)
                .NotEmpty().WithMessage("Content Type is required.")
                .NotNull().WithMessage("Content Type cant be Null")
                .Must(type => type == "Video" || type == "Document" || type == "Quiz").WithMessage("Content Type must be either 'Video', 'Document' or 'Quiz'.");
        }
    }
}
