using MediatR;

namespace Application_Layer.Commands.CourseCommands.DeleteCourse
{
    public class DeleteCourseCommand : IRequest<DeleteCourseResult>
    {
        public string CourseId { get; }

        public DeleteCourseCommand(string courseId)
        {
            CourseId = courseId;
        }
    }
}
