using Gym.Core.Models;
using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;
using System;

namespace Gym.Infrastructure.Handlers.Result
{
    public class DeleteCardioResultHandler : ICommandHandler<DeleteCommand>
    {
        private readonly ICardioResultService _cardioResultService;

        public DeleteCardioResultHandler(ICardioResultService cardioResultService)
        {
            _cardioResultService = cardioResultService;
        }

        public void Handle(DeleteCommand command)
        {
            _cardioResultService.Delete(command.Id);
        }
    }
}
