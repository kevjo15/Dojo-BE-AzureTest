using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class DescriptionValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Description is required.")
                .Length(1, 500).WithMessage("Description must be between 1 and 500 characters.");
        }
    }
}
