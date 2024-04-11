using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class IdValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Id cant be NULL")
                .NotEmpty().WithMessage("The identifier can't be empty.")
                .Must(BeAValidGuid).WithMessage("The identifier must be a valid GUID.");
        }
        private static bool BeAValidGuid(string guidString)
        {
            return Guid.TryParse(guidString, out _);
        }
    }
}
