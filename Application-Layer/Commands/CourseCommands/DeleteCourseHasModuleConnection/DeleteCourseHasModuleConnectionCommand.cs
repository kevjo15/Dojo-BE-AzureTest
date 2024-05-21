using MediatR;

namespace Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection
{
    public class DeleteCourseHasModuleConnectionCommand : IRequest<DeleteCourseHasModuleConnectionResult>
    {
        public string CourseId { get; }
        public string ModuleId { get; }
        public DeleteCourseHasModuleConnectionCommand(string courseId, string moduleId)
        {
            CourseId = courseId;
            ModuleId = moduleId;
        }
    }
}
