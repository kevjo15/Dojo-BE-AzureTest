using Domain_Layer.Models.CourseModel;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria
{
    public class GetAllCoursesBySearchCriteriaQueryHandler : IRequestHandler<GetAllCoursesBySearchCriteriaQuery, List<CourseModel>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetAllCoursesBySearchCriteriaQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<List<CourseModel>> Handle(GetAllCoursesBySearchCriteriaQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SearchCriteria))
            {
                throw new ArgumentException($"Course with searched criteria: {request.SearchCriteria}, was not found!");
            }
            var course = await _courseRepository.GetCoursesBySearchCriteria(request.SearchCriteria);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with searched criteria: {request.SearchCriteria}, was not found!");
            }

            return course;
        }
    }
}
