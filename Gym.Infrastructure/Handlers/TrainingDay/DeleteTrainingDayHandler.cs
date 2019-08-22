using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.TrainingDay;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.TrainingDay
{
    public class DeleteTrainingDayHandler : ICommandHandler<DeleteCommand>
    {
        private readonly ITrainingDayService _trainingDayService;

        public DeleteTrainingDayHandler(ITrainingDayService trainingDayService, ITrainingPlanService trainingPlanService)
        {
            _trainingDayService = trainingDayService;
        }

        public void Handle(DeleteCommand command)
        {
            _trainingDayService.Delete(command.Id);
        }
    }
}
