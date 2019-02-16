using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class CreateResultHandler : ICommandHandler<CreateResult>
    {
        private readonly IWeightResultService _weightResultService;
        private readonly ICardioResultService _cardioResultService;
        private readonly ITrainingDayService _trainingDayService;
        private readonly IExerciseService _exerciseService;

        public CreateResultHandler(IWeightResultService weightResultService, ICardioResultService cardioResultService, ITrainingDayService trainingDayService, IExerciseService exerciseService)
        {
            _weightResultService = weightResultService;
            _cardioResultService = cardioResultService;
            _trainingDayService = trainingDayService;
            _exerciseService = exerciseService;
        }

        public void Handle(CreateResult command)
        {
            var exercise = _exerciseService.Get(command.ExerciseId);
            var trainingDay = _trainingDayService.Get(command.TrainingDayId);

            if (exercise.Category == Category.Cardio)
                _cardioResultService.CreateNew(trainingDay, exercise, command.Distance, command.Time);
            else
                _weightResultService.CreateNew(trainingDay, exercise, command.Series, command.Weight, command.Reps);
        }
    }
}
