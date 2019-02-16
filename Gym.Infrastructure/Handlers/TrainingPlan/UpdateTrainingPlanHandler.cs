using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingPlan;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingPlan
{
    public class UpdateTrainingPlanHandler : ICommandHandler<UpdateTrainingPlan>
    {
        private readonly ITrainingPlanService _trainingPlanService;

        public UpdateTrainingPlanHandler(ITrainingPlanService trainingPlanService)
        {
            _trainingPlanService = trainingPlanService;
        }

        public void Handle(UpdateTrainingPlan command)
        {
            _trainingPlanService.Update(command.Id, command.Name, command.ExercisesId);
        }
    }
}
