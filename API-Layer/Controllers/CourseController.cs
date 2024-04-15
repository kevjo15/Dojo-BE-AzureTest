using Application_Layer.Commands.CourseCommands;
using Application_Layer.Commands.CourseCommands.DeleteCourse;
using Application_Layer.DTO_s;
using Application_Layer.Queries.CourseQueries.GetCourseById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : BaseController
    {
        public CourseController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO courseDTO)
        {
            try
            {
                var result = await _mediator.Send(new CreateCourseCommand(courseDTO));

                return CreateResponse(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetCourseById/{courseId}")]
        public async Task<IActionResult> GetCourseById(string courseId)
        {
            var result = await _mediator.Send(new GetCourseByIdQuery(courseId));

            return CreateResponse(result);
        }

        [HttpDelete("DeleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(string courseId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCourseCommand(courseId));

                return CreateResponse(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
