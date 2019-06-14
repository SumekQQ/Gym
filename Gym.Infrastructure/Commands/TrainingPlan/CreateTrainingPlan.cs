using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Commands.TrainingPlan
{
    public class CreateTrainingPlan : ICommand
    {
        public string Name { get; set; }
        public List<Guid> ExerciseIds { get; set; }
    }
}
