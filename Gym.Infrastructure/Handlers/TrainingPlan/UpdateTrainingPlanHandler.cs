using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class UpdateTrainingPlanHandler : ICommandHandler<UpdateTrainingPlan>
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public UpdateTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public async Task Handle(UpdateTrainingPlan command)
        {
            await _trainingPlanService.Update(command.Id, command.Name, command.ExerciseIds);
        }
    }
}
