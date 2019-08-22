using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class DeleteWeightResultHandler : ICommandHandler<DeleteCommand>
    {
        private readonly IWeightResultService _weightResultService;

        public DeleteWeightResultHandler(IWeightResultService weightResultService)
        {
            _weightResultService = weightResultService;
        }

        public void Handle(DeleteCommand command)
        {
            _weightResultService.Delete(command.Id);
        }
    }
}
