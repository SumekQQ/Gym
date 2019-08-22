using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
       public async Task<ActionResult> GetWeightResult(Guid id)
        {
            return await GetSingle(await _weightResultService.Get(id));
        }

        [Route("/cardio/{id}")]
        [HttpGet]
       public async Task<ActionResult> GetCardioResult(Guid id)
        {
            return await GetSingle(await _cardioResultService.Get(id));
        }

        [HttpPost]
       public async Task<ActionResult> CreateNew([FromBody] CreateWeightResult command)
        {
            return await Post(command);
        }

        [Route("/weight/{id}")]
        [HttpPut]
       public async Task<ActionResult> Update([FromBody] UpdateWeightResult command)
        {
            return await Put(command);
        }

        [Route("/cardio/{id}")]
        [HttpPut]
       public async Task<ActionResult> Update([FromBody] UpdateCardioResult command)
        {
            return await Put(command);
        }

        [Route("/weight/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteWeightResult command)
        {
            return await Delete(command);
        }

        [Route("/cardio/{id}")]
        [HttpDelete]
        public async Task<ActionResult> Delete([FromBody] DeleteCardioResult command)
        {
            return await Delete(command);
        }
    }
}