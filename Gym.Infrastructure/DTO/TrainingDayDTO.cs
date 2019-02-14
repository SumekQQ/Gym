using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.DTO
{
    public class TrainingDayDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public TrainingPlan TrainingPlan { get; set; }
        public string Description { get; set; }
    }
}
