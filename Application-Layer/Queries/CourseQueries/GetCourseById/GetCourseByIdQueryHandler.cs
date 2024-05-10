using Domain_Layer.Models.Course;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Queries.CourseQueries.GetCourseById
{
    public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseModel>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CourseModel> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CourseId))
            {
                throw new ArgumentException($"Course with ID {request.CourseId} was not found!");
            }
            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
            if (course == null)
            {
                throw new KeyNotFoundException($"Course with ID {request.CourseId} was not found!");
            }

            return course;
        }
    }
}
