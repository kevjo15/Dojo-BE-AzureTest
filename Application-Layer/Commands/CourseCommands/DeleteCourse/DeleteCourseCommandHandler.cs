using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Commands.CourseCommands.DeleteCourse
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, DeleteCourseResult>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<DeleteCourseResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CourseId))
            {
                throw new ArgumentException("CourseId cannot be empty!");
            }
            try
            {
                await _courseRepository.DeleteCourseByIdAsync(request.CourseId);

                return new DeleteCourseResult { Success = true, Message = "Course and related modules successfully deleted" };
            }
            catch (Exception ex)
            {
                return new DeleteCourseResult { Success = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
