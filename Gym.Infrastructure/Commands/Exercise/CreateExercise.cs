﻿using Gym.Core.Models;

namespace Gym.Infrastructure.Commands.Exercise
{
    public class CreateExercise : ICommand
    {
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}
