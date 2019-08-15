using System;

namespace Gym.Infrastructure.Commands.TrainingDay
{
    public class CreateTrainingDay : ICommand
    {
        public Guid TrainingPlanId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; } 
    }
}
