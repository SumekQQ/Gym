using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class DeleteTrainingDayHandler : ICommandHandler<DeleteTrainingDay>
    {
        private readonly ITrainingDayService _trainingDayService;

        public DeleteTrainingDayHandler(ITrainingDayService trainingDayService, ITrainingPlanService trainingPlanService)
        {
            _trainingDayService = trainingDayService;
        }

        public async Task Handle(DeleteTrainingDay command)
        {
            await _trainingDayService.Delete(command.TrainingPlanId);
        }
    }
}
