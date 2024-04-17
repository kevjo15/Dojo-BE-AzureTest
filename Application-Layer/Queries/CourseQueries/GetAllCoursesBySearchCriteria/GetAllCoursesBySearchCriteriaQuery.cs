using Domain_Layer.Models.CourseModel;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria
{
    public class GetAllCoursesBySearchCriteriaQuery : IRequest<List<CourseModel>>
    {
        public SearchCriteria SearchCriteriaInfo { get; }
        public GetAllCoursesBySearchCriteriaQuery(SearchCriteria _searchCriteria)
        {
            SearchCriteriaInfo = _searchCriteria;
        }
    }
}
