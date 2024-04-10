using Application_Layer.Commands.CourseCommands.DeleteCourse;
using Application_Layer.Commands.CourseCommands.UpdateCourse;
using Application_Layer.DTO_s;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpDelete("DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCourseCommand(courseId));

                if (result.Success)
                {
                    return Ok(result.Message);
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(string courseId, [FromBody] CourseUpdateDTO courseUpdateDTO)
        {
            var command = new UpdateCourseCommand(courseUpdateDTO, courseId);

            var updatedCourse = await _mediator.Send(command);

            if (updatedCourse == null)
            {
                return NotFound($"Course with ID {courseId} not found.");
            }

            return Ok(updatedCourse);
        }
    }
}
