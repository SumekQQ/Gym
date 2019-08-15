using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class DeleteExerciseHandler : ICommandHandler<DeleteExercise>
    {
        private readonly IExerciseService _exerciseService;

        public DeleteExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public void Handle(DeleteExercise command)
        {
            _exerciseService.Delete(command.Id);
        }
    }
}