using Application_Layer.Validators.ValidationExtensions;
using FluentValidation;

namespace Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria
{
    public class GetAllCoursesBySearchCriteriaQueryValidator : AbstractValidator<GetAllCoursesBySearchCriteriaQuery>
    {
        public GetAllCoursesBySearchCriteriaQueryValidator()
        {
            RuleFor(course => course.SearchCriteriaInfo.CourseId)!
                .MustBeValidSearchTerm();
            RuleFor(course => course.SearchCriteriaInfo.Title)!
                .MustBeValidSearchTerm();
            RuleFor(course => course.SearchCriteriaInfo.CategoryOrSubject)!
                .MustBeValidSearchTerm();
            RuleFor(course => course.SearchCriteriaInfo.Language)!
                .MustBeValidSearchTerm();
            RuleFor(course => course.SearchCriteriaInfo.FirstName)!
                .MustBeValidSearchTerm();
            RuleFor(course => course.SearchCriteriaInfo.LastName)!
                .MustBeValidSearchTerm();
        }
    }
}
