using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse
{
    public class GetAllModulesByCourseIdQueryValidator : AbstractValidator<GetAllModulesByCourseIdQuery>
    {
        public GetAllModulesByCourseIdQueryValidator()
        {
            RuleFor(module => module.CourseId).MustBeValidId();
        }
    }
}
