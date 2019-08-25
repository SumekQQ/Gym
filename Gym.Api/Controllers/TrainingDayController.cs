using Gym.Core.Repositories;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gym.Api.Controllers
{
    public class TrainingDayController : BaseController
    {
        private readonly ITrainingDayService _trainingDayService;

        public TrainingDayController(ITrainingDayService trainingDayService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _trainingDayService = trainingDayService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return await GetCollection(await _trainingDayService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return await GetSingle(await _trainingDayService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] CreateTrainingDay command)
        {
            return await Post(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTrainingDay command)
        {
            return await Put(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] DeleteTrainingDay command)
        {
            return await Remove(command);
        }
    }
}