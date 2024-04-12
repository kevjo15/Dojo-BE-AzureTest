
using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria
{
    public class GetAllCoursesBySearchCriteriaQueryValidator : AbstractValidator<GetAllCoursesBySearchCriteriaQuery>
    {
        public GetAllCoursesBySearchCriteriaQueryValidator()
        {
            RuleFor(course => course.SearchCriteria)
           .MustBeValidSearchCriteria();
        }
    }
}
