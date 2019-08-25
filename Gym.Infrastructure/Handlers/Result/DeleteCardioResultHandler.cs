using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Result
{
    public class DeleteCardioResultHandler : ICommandHandler<DeleteCardioResult>
    {
        private readonly ICardioResultService _cardioResultService;

        public DeleteCardioResultHandler(ICardioResultService cardioResultService)
        {
            _cardioResultService = cardioResultService;
        }

        public async Task Handle(DeleteCardioResult command)
        {
            await _cardioResultService.Delete(command.Id);
        }
    }
}
