using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Result
{
    public class UpdateCardioResultHandler : ICommandHandler<UpdateCardioResult>
    {
        private readonly ICardioResultService _cardioResultService;

        public UpdateCardioResultHandler(ICardioResultService cardioResultService)
        {
            _cardioResultService = cardioResultService;
        }

        public async Task Handle(UpdateCardioResult command)
        {
            await _cardioResultService.Update(command.Id, command.Distance, command.Time);
        }
    }
}
