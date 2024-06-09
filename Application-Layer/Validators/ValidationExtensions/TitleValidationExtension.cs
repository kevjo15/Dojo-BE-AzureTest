using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class TitleValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidTitle<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title must be between 1 and 100 characters.");
        }
    }
}
