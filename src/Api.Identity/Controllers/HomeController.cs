using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Identity.Controllers
{
    [Route("")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"Hello from: {Assembly.GetAssembly(typeof(HomeController)).GetName()}");
        }
    }
}
