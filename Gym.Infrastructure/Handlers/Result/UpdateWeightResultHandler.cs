using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System;

namespace Gym.Infrastructure.Handlers.Result
{
    public class UpdateWeightResultHandler : ICommandHandler<UpdateWeightResult>
    {
        private readonly IWeightResultService _weightResultService;
        private readonly ITrainingDayService _trainingDayService;
        private readonly IExerciseService _exerciseService;

        public UpdateWeightResultHandler(IWeightResultService weightResultService, ITrainingDayService trainingDayService, IExerciseService exerciseService)
        {
            _weightResultService = weightResultService;
            _trainingDayService = trainingDayService;
            _exerciseService = exerciseService;
        }

        public void Handle(UpdateWeightResult command)
        {
            var resultExist = _weightResultService.Get(command.Id);

            _weightResultService.Update(resultExist, command.Series, command.Weight, command.Reps);
        }
    }
}
