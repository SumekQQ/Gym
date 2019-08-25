using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        public async Task<ActionResult> Get()
        {
            return await GetCollection(await _exerciseService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            return await GetSingle(await _exerciseService.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateNew([FromBody] CreateExercise command)
        {
            return await Post(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateExercise command)
        {
            return await Put(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromBody] DeleteExercise command)
        {
            return await Remove(command);
        }
    }
}