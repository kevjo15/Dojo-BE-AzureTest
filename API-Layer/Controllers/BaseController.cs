using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers;

public abstract class BaseController : ControllerBase
{
    internal readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected IActionResult CreateResponse<T>(T result) where T : class
    {
        if (result == null) return BadRequest(result);

        var successProp = result.GetType().GetProperty("Success");
        var messageProp = result.GetType().GetProperty("Message");

        var success = (bool)successProp.GetValue(result);
        var message = messageProp.GetValue(result)?.ToString();

        if (success)
        {
            return Ok(message);
        }
        else
        {
            return BadRequest(message);
        }
    }
}
