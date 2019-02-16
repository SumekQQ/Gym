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

        public CreateTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public void Handle(CreateTrainingPlan command)
        {
            _trainingPlanService.CreateNew(command.Name, command.ExercisesId);
        }
    }
}
