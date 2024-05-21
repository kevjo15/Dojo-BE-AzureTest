using Application_Layer.Commands.ModuleCommands.CreateModule;
using Application_Layer.Commands.ModuleCommands.DeleteModule;
using Application_Layer.Commands.ModuleCommands.UpdateModule;
using Application_Layer.DTO_s.Module;
using Application_Layer.Queries.ModuleQueries.GetAllModulesByCourse;
using Application_Layer.Queries.ModuleQueries.GetModuleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ModuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateModule")]
        public async Task<IActionResult> CreateModule([FromBody] CreateModuleDTO moduleDTO)
        {
            try
            {
                var result = await _mediator.Send(new CreateModuleCommand(moduleDTO));
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


        [HttpDelete("DeleteModule/{moduleId}")]
        public async Task<IActionResult> DeleteModule(string moduleId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteModuleCommand(moduleId));

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

        [HttpGet("GetAllModulesByCourseId/{courseId}")]
        public async Task<IActionResult> GetAllModulesByCourseId(string courseId)
        {
            try
            {
                var modules = await _mediator.Send(new GetAllModulesByCourseIdQuery(courseId));
                if (modules == null)
                {
                    return NotFound($"Course with ID {courseId} was not found.");
                }
                return Ok(modules);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetModuleById/{moduleId}")]
        public async Task<IActionResult> GetModuleById(string moduleId)
        {
            {
                var module = await _mediator.Send(new GetModuleByIdQuery(moduleId));
                if (module != null)
                {
                    return Ok(module);
                }
                else
                {
                    return NotFound($"Module with ID {moduleId} was not found.");
                }
            }
        }

        [HttpPut("UpdateModule/{moduleId}")]
        public async Task<IActionResult> UpdateModule(string moduleId, [FromBody] UpdateModuleDTO moduleDto)
        {
            try
            {
                if (moduleDto == null)
                {
                    return BadRequest("Invalid request: No data provided.");
                }

                var command = new UpdateModuleCommand(moduleId, moduleDto);

                var result = await _mediator.Send(command);

                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
