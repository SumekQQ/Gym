using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public ActionResult GetAll()
        {
            return Collection(_trainingPlanService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            return Single(_trainingPlanService.Get(id));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateTrainingPlan command)
        {
            Dispatch(command);

            return Created($"get/{command.Name}", null);
        }

        [HttpPut]
        public ActionResult Update([FromBody] UpdateTrainingPlan command)
        {
            Dispatch(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _trainingPlanService.Delete(id);

            return Ok();
        }
    }
}