using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.DTO
{
    public class WeightResultDTO
    {
        public Guid Id { get; set; }
        public TrainingDay TrainingDay { get; set; }
        public Exercise Exercise { get; set; }
        public int Series { get; set; }
        public float Weight { get; set; }
        public int Reps { get; set; }
    }
}
