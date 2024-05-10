using Application_Layer.DTO_s;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application_Layer.Commands.CourseCommands.UpdateCourse
{
    public class UpdateCourseCommand : IRequest<IActionResult>
    {
        public string CourseId { get; set; }
        public CourseUpdateDTO CourseUpdateDTO { get; set; }

        public UpdateCourseCommand(CourseUpdateDTO courseUpdateDTO, string courseId)
        {
            CourseUpdateDTO = courseUpdateDTO;
            CourseId = courseId;
        }
    }
}
