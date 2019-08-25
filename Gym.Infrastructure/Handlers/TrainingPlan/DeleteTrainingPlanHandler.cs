using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class DeleteTrainingPlanHandler : ICommandHandler<DeleteTrainingPlan>
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public DeleteTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public async Task Handle(DeleteTrainingPlan command)
        {
           await _trainingPlanService.Delete(command.Id);
        }
    }
}
