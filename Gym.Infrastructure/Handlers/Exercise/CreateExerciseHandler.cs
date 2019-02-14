using Gym.Infrastructure.Commands;
using Gym.Infrastructure.Commands.Exercise;
using Gym.Infrastructure.Services;

namespace Gym.Infrastructure.Handlers.Exercise
{
    public class CreateExerciseHandler : ICommandHandler<CreateExercise>
    {
        private readonly IExerciseService _exerciseService;

        public CreateExerciseHandler(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        public void Handle(CreateExercise command)
        {
            _exerciseService.CreateNew(command.Name, command.BodyPart);
        }
    }
}
