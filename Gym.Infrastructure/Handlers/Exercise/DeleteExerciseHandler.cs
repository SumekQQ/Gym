using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class DeleteExerciseHandler : ICommandHandler<DeleteExercise>
    {
        private readonly IExerciseService _exerciseService;

        public DeleteExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public async Task Handle(DeleteExercise command)
        {
           await _exerciseService.Delete(command.ExerciseId);
        }
    }
}