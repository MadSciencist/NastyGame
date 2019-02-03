using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Api.Statistics.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($@"Hello from: {Assembly.GetAssembly(typeof(HomeController)).GetName()}");
        }
    }
}