using Gym.Core.Models;

namespace Gym.Infrastructure.Commands.Exercise
{
    public class CreateExercise : ICommand
    {
        public string Name { get; set; }
        public BodyPart BodyPart { get; set; }
    }
}
