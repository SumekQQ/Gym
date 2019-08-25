using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class CreateExerciseHandler : ICommandHandler<CreateExercise>
    {
        private readonly IExerciseService _exerciseService;

        public CreateExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task Handle(CreateExercise command)
        {
           await _exerciseService.CreateNew(command.Name, command.Category);
        }
    }
}
