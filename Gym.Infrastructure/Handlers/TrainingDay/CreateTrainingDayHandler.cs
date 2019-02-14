using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class CreateTrainingDayHandler : ICommandHandler<CreateTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;
        private readonly ITrainingPlanService _trainingPlanService;

        public CreateTrainingDayHandler(ITrainingDayService trainingDayService, ITrainingPlanService trainingPlanService)
        {
            _trainingDayService = trainingDayService;
            _trainingPlanService = trainingPlanService;
        }

        public void Handle(CreateTrainingDay command)
        {
            var trainingPlan = _trainingPlanService.Get(command.TrainingPlanId);

            _trainingDayService.CreateNew(trainingPlan, command.Description);
        }
    }
}
