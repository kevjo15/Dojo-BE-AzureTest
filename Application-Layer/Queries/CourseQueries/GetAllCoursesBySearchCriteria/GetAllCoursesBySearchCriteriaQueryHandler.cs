using Domain_Layer.Models.Course;
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
            try
            {
                var searchCriteria = request.SearchCriteriaInfo;

                // Trim and check if search criteria are not empty or whitespace
                var courseId = searchCriteria.CourseId?.Trim();
                var title = searchCriteria.Title?.Trim();
                var categoryOrSubject = searchCriteria.CategoryOrSubject?.Trim();
                var language = searchCriteria.Language?.Trim();
                var firstName = searchCriteria.FirstName?.Trim();
                var lastName = searchCriteria.LastName?.Trim();

                // Combine first and last name for the user search
                var fullName = $"{firstName} {lastName}".Trim();

                // If there is no search criteria, we are returning all courses
                if (searchCriteria.SearchBySearchTerm == false)
                {
                    return await _courseRepository.GetAllCourses();
                }

                var searchedList = await _courseRepository.GetCoursesBySearchCriteria(searchCriteria);
                if (!searchedList.Any())
                {
                    throw new InvalidOperationException("No courses found matching the search term.");
                }

                return searchedList;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching courses: {ex.Message}", ex);
            }
        }
    }
}
