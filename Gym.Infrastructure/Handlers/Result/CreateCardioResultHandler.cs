using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class CreateCardioResultHandler : ICommandHandler<CreateCardioResult>
    {
        private readonly ICardioResultService _cardioResultService;

        public CreateCardioResultHandler(ICardioResultService cardioResultService)
        {
            _cardioResultService = cardioResultService;
        }

        public void Handle(CreateCardioResult command)
        {
            _cardioResultService.CreateNew(command.TrainingDayId, command.ExerciseId, command.Distance, command.Time);
        }
    }
}
