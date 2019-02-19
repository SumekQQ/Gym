using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gym.Api.Controllers
{
    public class ResultController : BaseController
    {
        private readonly IWeightResultService _weightResultService;
        private readonly ICardioResultService _cardioResultService;

        public ResultController(IWeightResultService weightResultService, ICardioResultService cardioResultService,
        ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _weightResultService = weightResultService;
            _cardioResultService = cardioResultService;
        }

        [Route("/weight/{id}")]
        [HttpGet]
        public ActionResult GetWeightResult(Guid id)
        {
            return Single(_weightResultService.Get(id));
        }

        [Route("/cardio/{id}")]
        [HttpGet]
        public ActionResult GetCardioResult(Guid id)
        {
            return Single(_cardioResultService.Get(id));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateWeightResult command)
        {
            Dispatch(command);

            return Ok();
        }

        [Route("/weight/{id}")]
        [HttpPut]
        public ActionResult Update([FromBody] UpdateWeightResult command)
        {
            Dispatch(command);

            return Ok();
        }

        [Route("/cardio/{id}")]
        [HttpPut]
        public ActionResult Update([FromBody] UpdateCardioResult command)
        {
            Dispatch(command);

            return Ok();
        }

        [Route("/weight/{id}")]
        [HttpDelete]
        public ActionResult Delete(Guid id)
        {
            _weightResultService.Delete(id);

            return Ok();
        }

        [Route("/cardio/{id}")]
        [HttpDelete]
        public ActionResult Delte(Guid id)
        {
            _cardioResultService.Delete(id);

            return Ok();
        }
    }
}