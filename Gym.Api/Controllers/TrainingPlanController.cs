using Gym.Core.Repositories;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gym.Api.Controllers
{
    public class TrainingPlanController : BaseController
    {
        private readonly ITrainingPlanRepository _trainingPlanRepository;

        public TrainingPlanController(ITrainingPlanRepository trainingPlanRepository,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _trainingPlanRepository = trainingPlanRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Collection(_trainingPlanRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            return Single(_trainingPlanRepository.Get(id));
        }

        [HttpGet]
        public ActionResult Get(string name)
        {
            return Single(_trainingPlanRepository.Get(name));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateTrainingPlan command)
        {
            Dispatch(command);

            return Created($"get/{command.Name}", null);
        }
    }
}