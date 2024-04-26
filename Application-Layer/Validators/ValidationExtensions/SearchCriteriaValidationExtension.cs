using System.Text.RegularExpressions;
using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class SearchCriteriaValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidSearchTerm<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotNull().WithMessage("Search criteria cant be NULL")
                .NotEmpty().WithMessage("Search criteria can't be empty.")
                .Must(BeValidGuidOrName)
                .WithMessage("Search term must be either a valid Id, teachers first name and last name, category, language or a title");
        }
        private static bool BeValidGuidOrName(string searchCriteria)
        {
            // Check if the search criteria is a valid GUID
            if (Guid.TryParse(searchCriteria, out _))
            {
                return true; // It's a valid GUID
            }

            if (searchCriteria.Length < 2 || searchCriteria.Length > 20)
            {
                return false; // Name must be between 2 and 20 characters
            }

            if (!Regex.IsMatch(searchCriteria, "^[a-zA-Z--/.:]+( [a-zA-Z]+)*$"))
            {
                return false; // Name can only contain letters
            }

            return true; // If none of the above conditions fail, the search criteria is considered valid
        }
    }
}
