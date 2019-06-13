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
            return Collection(_exerciseService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            return Single(_exerciseService.Get(id));
        }

        [HttpGet("{category}")]
        public ActionResult Get(Category category)
        {
            return Single(_exerciseService.Get(category));
        }

        [HttpPost]
        public ActionResult CreateNew([FromBody] CreateExercise command)
        {
            Dispatch(command);

            return Created($"get/{command.Name}", null);
        }

        [HttpPut]
        public ActionResult Update([FromBody] UpdateExercise command)
        {
            Dispatch(command);

            return Created($"get/{command.Name}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            _exerciseService.Delete(id);

            return Ok();
        }
    }
}