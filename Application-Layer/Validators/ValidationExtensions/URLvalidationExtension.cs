
using FluentValidation;

namespace Application_Layer.Validators.ValidationExtensions
{
    public static class URLvalidationExtension
    {
        public static IRuleBuilderOptions<T, string> MustBeValidURL<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
               .Must(BeAValidUrl).WithMessage("URL is not valid.");
        }
        private static bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            Uri uriResult;
            return Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
