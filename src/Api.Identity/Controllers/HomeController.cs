using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Identity.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok($"Hello from: {Assembly.GetAssembly(typeof(HomeController)).GetName()}");
        }
    }
}
