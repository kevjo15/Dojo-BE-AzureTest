using Application_Layer.Commands.CourseCommands.CreateCourse;
using Application_Layer.DTO_s;
using MediatR;

namespace Application_Layer.Commands.CourseCommands
{
    public class CreateCourseCommand : IRequest<CreateCourseResult>
    {
        public CreateCourseDTO CourseDTO { get; }

        public CreateCourseCommand(CreateCourseDTO courseDTO)
        {
            CourseDTO = courseDTO;
        }
    }
}
