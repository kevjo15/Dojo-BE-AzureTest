using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
