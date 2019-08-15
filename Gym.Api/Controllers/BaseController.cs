using Gym.Infrastructure.Commands;
using Gym.Core.Exceptions;
using Gym.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        protected ActionResult GetSingle<T>(T data)
        {
            if (data == null)
                return NotFound();

            return Json(data);
        }

        protected ActionResult GetCollection<T>(IEnumerable<T> data)
        {
            if (data.Count() == 0)
                return NotFound();

            return Json(data);
        }

        protected ActionResult Post<T>(T command) where T : ICommand
        {
            return dispatch(command, true);
        }

        protected ActionResult Put<T>(T command) where T : ICommand
        {
            return dispatch(command);
        }

        protected ActionResult Delete<T>(T command) where T : ICommand
        {
            return dispatch(command);
        }

        private ActionResult dispatch<T>(T command, bool isPost = false) where T : ICommand
        {
            try
            {
                _commandDispatcher.Dispatch(command);

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