using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Hub.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"Hello from: {Assembly.GetAssembly(typeof(HomeController)).GetName()}");
        }
    }
}