using System;

namespace Gym.Infrastructure.Commands.Exercise
{
    public class DeleteExercise : ICommand
    {
        public Guid Id { get; set; }

        public DeleteExercise(Guid id)
        {
            Id = id;
        }
    }
}
