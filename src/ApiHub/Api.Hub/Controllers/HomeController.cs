using Api.Common.Messaging.Abstractions;
using Api.Hub.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace Api.Hub.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEventBus _eventBus;

        public HomeController(IEventBus _eventBus)
        {
            this._eventBus = _eventBus;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var evt = new UpdateUserKillsEvent(551, "Victim");
            _eventBus.Publish(evt);
            var evt3 = new PlayerStartedNewGameEvent(1, DateTime.Now);
            _eventBus.Publish(evt3);
            var evt2 = new UpdateUserDeathsEvent("master", 51, TimeSpan.Zero);
            _eventBus.Publish(evt2);

   


            return Ok($"Hello from: {Assembly.GetAssembly(typeof(HomeController)).GetName()}");
        }
    }
}