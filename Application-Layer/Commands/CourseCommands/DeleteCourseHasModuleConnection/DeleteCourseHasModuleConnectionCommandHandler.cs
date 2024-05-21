using Application_Layer.Commands.CourseCommands.DeleteCourse;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection
{
    public class DeleteCourseHasModuleConnectionCommandHandler : IRequestHandler<DeleteCourseHasModuleConnectionCommand, DeleteCourseHasModuleConnectionResult>
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseHasModuleConnectionCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<DeleteCourseHasModuleConnectionResult> Handle(DeleteCourseHasModuleConnectionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CourseId) || string.IsNullOrWhiteSpace(request.ModuleId))
            {
                return new DeleteCourseHasModuleConnectionResult { Success = false, Message = "CourseId or ModuleId cannot be empty!" };
            }

            try
            {
                await _courseRepository.DeleteCourseHasModuleConnection(request.CourseId, request.ModuleId);

                return new DeleteCourseHasModuleConnectionResult { Success = true, Message = "Connection is successfully deleted" };
            }
            catch (Exception ex)
            {
                return new DeleteCourseHasModuleConnectionResult { Success = false, Message = $"An error occurred: {ex.Message}" };
            }
        }
    }
}
