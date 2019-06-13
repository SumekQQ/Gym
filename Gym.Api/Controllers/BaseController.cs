using Gym.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Gym.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public abstract class BaseController : Controller
    {
        private readonly ICommandDispatcher _commandDispatcher;

        protected BaseController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        protected ActionResult Single<T>(T data)
        {
            if (data == null)
                return NotFound();

            return Json(data);
        }

        protected ActionResult Collection<T>(IEnumerable<T> data)
        {
            if (data.Count() == 0)
                return NotFound();

            return Json(data);
        }

        protected void Dispatch<T>(T command) where T : ICommand
        {
            _commandDispatcher.Dispatch(command);
        }
    }
}