using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class UpdateExerciseHandler : ICommandHandler<UpdateExercise>
    {
        private readonly IExerciseService _exerciseService;

        public UpdateExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public void Handle(UpdateExercise command)
        {
            _exerciseService.Update(command.Id, command.Name, command.Category);
        }
    }
}
