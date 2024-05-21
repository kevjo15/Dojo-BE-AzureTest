using Domain_Layer.CommandOperationResult;
using Infrastructure_Layer.Repositories.Course;
using MediatR;

namespace Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection
{
    public class CreateCourseHasModuleConnectionCommandHandler : IRequestHandler<CreateCourseHasModuleConnectionCommand, OperationResult<bool>>
    {
        private readonly ICourseRepository _courseRepository;
        public CreateCourseHasModuleConnectionCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<OperationResult<bool>> Handle(CreateCourseHasModuleConnectionCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.CourseId) || string.IsNullOrEmpty(request.ModuleId))
            {
                return new OperationResult<bool> { Success = false, Message = "Invalid course or module ID." };
            }

            var result = await _courseRepository.ConnectCourseWithModuleAsync(request.CourseId, request.ModuleId);

            if (!result.Success)
            {
                return new OperationResult<bool> { Success = false, Message = result.Message };
            }

            return new OperationResult<bool> { Success = true, Message = "Module is successfully connected to Course" };
        }
    }

}
