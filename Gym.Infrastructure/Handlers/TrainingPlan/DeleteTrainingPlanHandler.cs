using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class DeleteTrainingPlanHandler : ICommandHandler<DeleteCommand>
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public DeleteTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public void Handle(DeleteCommand command)
        {
            _trainingPlanService.Delete(command.Id);
        }
    }
}
