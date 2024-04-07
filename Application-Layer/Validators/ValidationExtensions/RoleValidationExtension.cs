using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class RoleValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidRole<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .NotEmpty().WithMessage("Role is required.")
               .NotNull().WithMessage("Role cant be Null");
        }
    }
}
