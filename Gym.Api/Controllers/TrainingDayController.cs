using Gym.Core.Repositories;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gym.Api.Controllers
{
    public class TrainingDayController : BaseController
    {
        private readonly ITrainingDayRepository _trainingDayRepository;

        public TrainingDayController(ITrainingDayRepository trainingDayRepository,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _trainingDayRepository = trainingDayRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Collection(_trainingDayRepository.GetAll());
        }

        [HttpGet]
        public ActionResult Get(Guid id)
        {
            return Single(_trainingDayRepository.Get(id));
        }

        [HttpGet]
        public ActionResult Get(DateTime date)
        {
            return Single(_trainingDayRepository.Get(date));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateTrainingDay command)
        {
            Dispatch(command);

            return Created($"get/", null);
        }
  
        [HttpPut]
        public ActionResult Update([FromBody] UpdateTrainingDay command)
        {
            return Created($"get/", null);
        }
    }
}