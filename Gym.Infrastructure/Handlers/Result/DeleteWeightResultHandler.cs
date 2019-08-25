using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Result
{
    public class DeleteWeightResultHandler : ICommandHandler<DeleteWeightResult>
    {
        private readonly IWeightResultService _weightResultService;

        public DeleteWeightResultHandler(IWeightResultService weightResultService)
        {
            _weightResultService = weightResultService;
        }

        public async Task Handle(DeleteWeightResult command)
        {
            await _weightResultService.Delete(command.Id);
        }
    }
}
