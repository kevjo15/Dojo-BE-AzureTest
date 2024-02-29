using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoStarterController : ControllerBase
    {
        [HttpGet]
        [Route("getCurrentTime")]
        public IActionResult DummyActionThatReturnsCurrentTime()
        {
            return Ok(DateTime.UtcNow);
        }
    }
}
