using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.GetUserByEmail
{
    public class GetUserByEmailValidator : AbstractValidator<GetUserByEmailQuery>
    {
        public GetUserByEmailValidator()
        {
            RuleFor(user => user.Email)
                .MustBeValidEmail();
        }
    }
}
