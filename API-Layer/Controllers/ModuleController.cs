using Application_Layer.Commands.ModuleCommands.CreateModule;
using Application_Layer.DTO_s.Module;
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


    }
}
