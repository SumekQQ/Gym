using Gym.Core.Models;
using System;

namespace Gym.Infrastructure.DTO
{
    public class TrainingDayDTO
    {
        public Guid Id { get; set; }
        public string Date { get; set; }
        public Guid TrainingPlan { get; set; }
        public string Description { get; set; }
    }
}
