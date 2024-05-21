using Application_Layer.Commands.CourseCommands;
using Application_Layer.Commands.CourseCommands.CreateCourseHasModuleConnection;
using Application_Layer.Commands.CourseCommands.DeleteCourse;
using Application_Layer.Commands.CourseCommands.DeleteCourseHasModuleConnection;
using Application_Layer.Commands.CourseCommands.UpdateCourse;
using Application_Layer.DTO_s;
using Application_Layer.Queries.CourseQueries.GetAllCoursesBySearchCriteria;
using Application_Layer.Queries.CourseQueries.GetCourseById;
using Domain_Layer.Models.Course;
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

        [HttpPost("SearchCourseBy")]
        public async Task<IActionResult> SearchCoursesBySearchTerm([FromBody] SearchCriteria searchCriteria)
        {
            try
            {
                return Ok(await _mediator.Send(new GetAllCoursesBySearchCriteriaQuery(searchCriteria)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO courseDTO)
        {
            try
            {
                var result = await _mediator.Send(new CreateCourseCommand(courseDTO));

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
        [HttpPost("course/{courseId}/add-module/{moduleId}")]
        public async Task<IActionResult> CreateCourseHasModulesConnection(string courseId, string moduleId)
        {
            try
            {
                var result = await _mediator.Send(new CreateCourseHasModuleConnectionCommand(courseId, moduleId));
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

        [HttpGet("GetCourseById/{courseId}")]
        public async Task<IActionResult> GetCourseById(string courseId)
        {
            var course = await _mediator.Send(new GetCourseByIdQuery(courseId));

            if (course != null)
            {
                return Ok(course);
            }
            else
            {
                return NotFound($"Course with ID {courseId} was not found.");
            }
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
        [HttpDelete("courses/{courseId}/remove-module/{moduleId}")]
        public async Task<IActionResult> DeleteCourseHasModuleConnection(string courseId, string moduleId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteCourseHasModuleConnectionCommand(courseId, moduleId));
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

        [HttpPut("UpdateCourse{courseId}")]
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
