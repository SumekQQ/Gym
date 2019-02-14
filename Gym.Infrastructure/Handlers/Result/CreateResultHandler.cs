using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class CreateResultHandler : ICommandHandler<CreateResult>
    {
        private readonly IResultService _resultService;
        private readonly ITrainingDayService _trainingDayService;
        private readonly IExerciseService _exerciseService;

        public CreateResultHandler(IResultService resultService, ITrainingDayService trainingDayService, IExerciseService exerciseService)
        {
            _resultService = resultService;
            _trainingDayService = trainingDayService;
            _exerciseService = exerciseService;
        }

        public void Handle(CreateResult command)
        {
            var exercise = _exerciseService.Get(command.ExerciseId);
            var trainingDay = _trainingDayService.Get(command.TrainingDayId);

            _resultService.CreateNew(trainingDay, exercise, command.Series, command.Weight, command.Reps);
        }
    }
}
