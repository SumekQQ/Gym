using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System;

namespace Gym.Infrastructure.Handlers.Result
{
    public class UpdateCardioResultHandler : ICommandHandler<UpdateCardioResult>
    {
        private readonly ICardioResultService _cardioResultService;
        private readonly ITrainingDayService _trainingDayService;
        private readonly IExerciseService _exerciseService;

        public UpdateCardioResultHandler(ICardioResultService cardioResultService, ITrainingDayService trainingDayService, IExerciseService exerciseService)
        {
            _cardioResultService = cardioResultService;
            _trainingDayService = trainingDayService;
            _exerciseService = exerciseService;
        }

        public void Handle(UpdateCardioResult command)
        {
            var resultExist = _cardioResultService.Get(command.Id);

            _cardioResultService.Update(resultExist, command.Distance, command.Time);
        }
    }
}
