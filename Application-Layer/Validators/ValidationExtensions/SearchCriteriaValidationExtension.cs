using System.Text.RegularExpressions;
using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class SearchCriteriaValidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidSearchCriteria<T>(this IRuleBuilder<T, string> ruleBuilder) 
        {
            return ruleBuilder
                .NotNull().WithMessage("Search criteria cant be NULL")
                .NotEmpty().WithMessage("Search criteria can't be empty.")
                .Must(BeValidGuidOrName)
                .WithMessage("Search criteria must be either a valid Id, teacher name, language or a title");
        }
        private static bool BeValidGuidOrName(string searchCriteria)
        {
            // Check if the search criteria is a valid GUID
            if (Guid.TryParse(searchCriteria, out _))
            {
                return true; // It's a valid GUID
            }

            // If not a valid GUID, check if it's a valid name
            // Reusing the logic from MustBeValidName for name validation
            //if (string.IsNullOrEmpty(searchCriteria))
            //{
            //    return false; // Name is required
            //}

            if (searchCriteria.Length < 2 || searchCriteria.Length > 20)
            {
                return false; // Name must be between 2 and 20 characters
            }

            if (!Regex.IsMatch(searchCriteria, "^[a-zA-Z]+$"))
            {
                return false; // Name can only contain letters
            }

            return true; // If none of the above conditions fail, the search criteria is considered valid
        }
    }
}


