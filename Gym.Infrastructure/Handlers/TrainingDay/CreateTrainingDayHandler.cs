using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class CreateTrainingDayHandler : ICommandHandler<CreateTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;

        public CreateTrainingDayHandler(ITrainingDayService trainingDayService)
        {
            _trainingDayService = trainingDayService;
        }

        public async Task Handle(CreateTrainingDay command)
        {
            await _trainingDayService.CreateNew(command.TrainingPlanId, command.Description, command.Date);
        }
    }
}
