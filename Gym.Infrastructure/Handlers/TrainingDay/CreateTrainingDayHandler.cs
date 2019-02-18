using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class CreateTrainingDayHandler : ICommandHandler<CreateTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;

        public CreateTrainingDayHandler(ITrainingDayService trainingDayService)
        {
            _trainingDayService = trainingDayService;
        }

        public void Handle(CreateTrainingDay command)
        {
            _trainingDayService.CreateNew(command.TrainingPlanId, command.Description);
        }
    }
}
