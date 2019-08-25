using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Result
{
    public class CreateCardioResultHandler : ICommandHandler<CreateCardioResult>
    {
        private readonly ICardioResultService _cardioResultService;

        public CreateCardioResultHandler(ICardioResultService cardioResultService)
        {
            _cardioResultService = cardioResultService;
        }

        public async Task Handle(CreateCardioResult command)
        {
           await _cardioResultService.CreateNew(command.TrainingDayId, command.ExerciseId, command.Distance, command.Time);
        }
    }
}
