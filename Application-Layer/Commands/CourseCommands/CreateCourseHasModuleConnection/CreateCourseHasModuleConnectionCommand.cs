using Domain_Layer.CommandOperationResult;
using MediatR;

namespace Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection
{
    public class CreateCourseHasModuleConnectionCommand : IRequest<OperationResult<bool>>
    {
        public string CourseId;
        public string ModuleId;
        public CreateCourseHasModuleConnectionCommand(string courseId, string moduleId)
        {
            CourseId = courseId;
            ModuleId = moduleId;
        }
    }
}
