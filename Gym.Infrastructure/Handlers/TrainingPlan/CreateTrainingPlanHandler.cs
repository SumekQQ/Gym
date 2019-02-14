using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Services;
using System.Collections.Generic;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class CreateTrainingPlanHandler : ICommandHandler<CreateTrainingPlan>
    {
        private readonly ITrainingPlanService _trainingPlanService;
        private readonly IExerciseService _exerciseService;

        public CreateTrainingPlanHandler(ITrainingPlanService trainingPlanService, IExerciseService exerciseService)
        {
            _trainingPlanService = trainingPlanService;
            _exerciseService = exerciseService;
        }

        public void Handle(CreateTrainingPlan command)
        {
            var exercises = new List<ExerciseDTO>();

            command.ExercisesId.ForEach(x => exercises.Add(_exerciseService.Get(x)));

            _trainingPlanService.CreateNew(command.Name, exercises);
        }
    }
}
