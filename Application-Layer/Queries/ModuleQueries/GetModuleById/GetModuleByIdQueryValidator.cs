using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.ModuleQueries.GetModuleById
{
    public class GetModuleByIdQueryValidator : AbstractValidator<GetModuleByIdQuery>
    {
        public GetModuleByIdQueryValidator()
        {
            RuleFor(query => query.ModuleId).MustBeValidId();
        }
    }
}
