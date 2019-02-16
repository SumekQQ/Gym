using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class UpdateTrainingDayHandler : ICommandHandler<UpdateTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;
        private readonly ITrainingPlanService _trainingPlanService;

        public UpdateTrainingDayHandler(ITrainingDayService trainingDayService, ITrainingPlanService trainingPlanService)
        {
            _trainingDayService = trainingDayService;
            _trainingPlanService = trainingPlanService;
        }

        public void Handle(UpdateTrainingDay command)
        {
            var trainingPlan = _trainingPlanService.Get(command.TrainingPlanId);

            _trainingDayService.Update(command.Id, trainingPlan, command.Description);
        }
    }
}
