using Application_Layer.Commands.CourseCommands.CreateCourse;
using Application_Layer.DTO_s;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Commands.CourseCommands
{
    public class CreateCourseCommand : IRequest<CreateCourseResult>
    {
        public CreateCourseDTO CreateCourseDTO { get; set; }

        public CreateCourseCommand(CreateCourseDTO createCourseDTO)
        {
            CreateCourseDTO = createCourseDTO;
        }
    }
}

