using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.Commands.Exercise
{
    public class DeleteExercise : ICommand
    {
        public Guid ExerciseId { get; set; }
    }
}
