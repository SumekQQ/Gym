//using Gym.Core.Repositories;
//using Gym.Infrastructure.Commands;
//using Gym.Infrastructure.Commands.TrainingDay;
//using Gym.Infrastructure.Services;
//using Microsoft.AspNetCore.Mvc;
//using System;

//namespace Gym.Api.Controllers
//{
//    public class TrainingDayController : BaseController
//    {
//        private readonly ITrainingDayService _trainingDayService;

//        public TrainingDayController(ITrainingDayService trainingDayService,
//            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
//        {
//            _trainingDayService = trainingDayService;
//        }

//        [HttpGet]
//        public ActionResult GetAll()
//        {
//            return Collection(_trainingDayService.GetAll());
//        }

//        [HttpGet("{id}")]
//        public ActionResult Get(Guid id)
//        {
//            return Single(_trainingDayService.Get(id));
//        }

//        [HttpPost]
//        public ActionResult CreateNew([FromBody] CreateTrainingDay command)
//        {
//            Dispatch(command);

//            return Created($"get/", null);
//        }
  
//        [HttpPut]
//        public ActionResult Update([FromBody] UpdateTrainingDay command)
//        {
//            return Created($"get/", null);
//        }

//        [HttpDelete("{id}")]
//        public ActionResult Delete(Guid id)
//        {
//            _trainingDayService.Delete(id);

//            return Ok();
//        }
//    }
//}