using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class UpdateTrainingDayHandler : ICommandHandler<UpdateTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;

        public UpdateTrainingDayHandler(ITrainingDayService trainingDayService, ITrainingPlanService trainingPlanService)
        {
            _trainingDayService = trainingDayService;
        }

        public void Handle(UpdateTrainingDay command)
        {
            _trainingDayService.Update(command.Id, command.TrainingPlanId, command.Description);
        }
    }
}
