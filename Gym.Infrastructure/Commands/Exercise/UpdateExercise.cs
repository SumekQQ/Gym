using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.Commands.Exercise
{
    public class UpdateExercise : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
