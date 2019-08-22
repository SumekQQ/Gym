using Gym.Infrastructure.Commands;
using Gym.Core.Exceptions;
using Gym.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

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

        protected async Task<ActionResult> GetSingle<T>(T data)
        {
            if (data == null)
                return NotFound();

            return Json(data);
        }

        protected async Task<ActionResult> GetCollection<T>(IEnumerable<T> data)
        {
            if (data.Count() == 0)
                return NotFound();

            return Json(data);
        }

        protected async Task<ActionResult> Post<T>(T command) where T : ICommand
        {
            return await dispatch(command, true);
        }

        protected async Task<ActionResult> Put<T>(T command) where T : ICommand
        {
            return await dispatch(command);
        }

        protected async Task<ActionResult> Delete<T>(T command) where T : ICommand
        {
            return await dispatch(command);
        }

        private async Task<ActionResult> dispatch<T>(T command, bool isPost = false) where T : ICommand
        {
            try
            {
                await _commandDispatcher.Dispatch(command);

                if (isPost)
                    return Created($"get", null);

                return Ok();
            }
            catch (ServiceException ex)
            {
                return Conflict(ExceptionExtension.PrintException(ex));
            }
            catch (DomainException ex)
            {
                return BadRequest(ExceptionExtension.PrintException(ex));
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}