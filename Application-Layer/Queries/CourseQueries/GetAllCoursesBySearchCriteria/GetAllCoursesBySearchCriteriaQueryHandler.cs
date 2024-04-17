using AutoMapper;
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
            if (request.SearchCriteriaInfo == null)
            {
                throw new ArgumentException("Course with searched criteria, was not found!");
            }
            // Use the repository to fetch courses based on the search criteria
            var courses = await _courseRepository.GetCoursesBySearchCriteria(request.SearchCriteriaInfo);
            if (courses == null)
            {
                throw new KeyNotFoundException("Course with searched criteria, was not found!");
            }

            // Return the list of courses
            return courses;
        }

    }
}
