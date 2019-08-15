using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Gym.Api.Controllers
{
    public class ExerciseController : BaseController
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(IExerciseService exerciseService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _exerciseService = exerciseService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return GetCollection(_exerciseService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            return GetSingle(_exerciseService.Get(id));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateExercise command)
        {
            return Post(command);
        }

        [HttpPut]
        public ActionResult Update([FromBody] UpdateExercise command)
        {
            return Put(command);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            return Delete(new DeleteExercise(id));
        }
    }
}