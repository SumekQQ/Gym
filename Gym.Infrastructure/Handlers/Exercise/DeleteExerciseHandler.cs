using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class DeleteExerciseHandler : ICommandHandler<DeleteCommand>
    {
        private readonly IExerciseService _exerciseService;

        public DeleteExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public void Handle(DeleteCommand command)
        {
            _exerciseService.Delete(command.Id);
        }
    }
}