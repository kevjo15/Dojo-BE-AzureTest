using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class UserIdValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidUserId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("UserId cant be NULL")
                .NotEmpty().WithMessage("The identifier can't be empty.")
                .Must(BeAValidGuid).WithMessage("The identifier must be a valid GUID.");
        }
        private static bool BeAValidGuid(string guidString)
        {
            return Guid.TryParse(guidString, out _);
        }
    }
}
