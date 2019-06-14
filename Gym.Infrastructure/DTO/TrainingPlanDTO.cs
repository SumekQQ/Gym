using Gym.Core.Models;
using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.DTO
{
    public class TrainingPlanDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> ExerciseIds { get; set; }
    }
}
