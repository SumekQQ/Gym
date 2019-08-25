using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.DTO;
using Gym.Infrastructure.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class CreateTrainingPlanHandler : ICommandHandler<CreateTrainingPlan>
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public CreateTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public async Task Handle(CreateTrainingPlan command)
        {
            await _trainingPlanService.CreateNew(command.Name, command.ExerciseIds);
        }
    }
}
