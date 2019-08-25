using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class UpdateExerciseHandler : ICommandHandler<UpdateExercise>
    {
        private readonly IExerciseService _exerciseService;

        public UpdateExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task Handle(UpdateExercise command)
        {
            await _exerciseService.Update(command.Id, command.Name, command.Category);
        }
    }
}
