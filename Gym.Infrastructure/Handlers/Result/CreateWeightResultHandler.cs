using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class CreateWeightResultHandler : ICommandHandler<CreateWeightResult>
    {
        private readonly IWeightResultService _weightResultService;

        public CreateWeightResultHandler(IWeightResultService weightResultService)
        {
            _weightResultService = weightResultService;
        }

        public void Handle(CreateWeightResult command)
        {
            _weightResultService.CreateNew(command.TrainingDayId, command.ExerciseId, command.Series, command.Weight, command.Reps);
        }
    }
}
