using Domain_Layer.Models.CourseModel;
using MediatR;

namespace Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria
{
    public class GetAllCoursesBySearchCriteriaQuery : IRequest<List<CourseModel>>
    {
        public string SearchCriteria { get; private set; }
        public GetAllCoursesBySearchCriteriaQuery(string _searchCriteria)
        {
            SearchCriteria = _searchCriteria;
        }
    }
}
