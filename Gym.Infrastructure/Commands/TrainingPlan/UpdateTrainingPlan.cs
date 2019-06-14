using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Commands.TrainingPlan
{
    public class UpdateTrainingPlan : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> ExerciseIds { get; set; }
    }
}
