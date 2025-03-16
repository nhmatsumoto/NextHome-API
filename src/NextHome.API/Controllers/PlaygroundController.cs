using Microsoft.AspNetCore.Mvc;

namespace NextHome.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlaygroundController : ControllerBase
    {
        [HttpGet]
        public IActionResult SimulateError()
        {
            throw new Exception("This is a test error!");
        }
    }
}
