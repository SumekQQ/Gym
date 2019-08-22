using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gym.Api.Controllers
{
    public class TrainingPlanController : BaseController
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public TrainingPlanController(ITrainingPlanService trainingPlanService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _trainingPlanService = trainingPlanService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return await GetCollection(await _trainingPlanService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return await GetSingle(await _trainingPlanService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] CreateTrainingPlan command)
        {
            return await Post(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateTrainingPlan command)
        {
            return await Put(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] DeleteCommand command)
        {
            return await Delete(command);
        }
    }
}