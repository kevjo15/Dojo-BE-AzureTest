using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(user => user.UserId)
                .MustBeValidUserId();
        }
    }
}
