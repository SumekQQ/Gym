using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Result;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Result
{
    public class UpdateWeightResultHandler : ICommandHandler<UpdateWeightResult>
    {
        private readonly IWeightResultService _weightResultService;

        public UpdateWeightResultHandler(IWeightResultService weightResultService)
        {
            _weightResultService = weightResultService;
        }

        public void Handle(UpdateWeightResult command)
        {
            _weightResultService.Update(command.Id, command.Series, command.Weight, command.Reps);
        }
    }
}
