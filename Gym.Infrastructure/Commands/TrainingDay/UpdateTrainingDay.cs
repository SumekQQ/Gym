using System;

namespace Gym.Infrastructure.Commands.TrainingDay
{
    public class UpdateTrainingDay : ICommand
    {
        public Guid Id { get; set; }
        public Guid TrainingPlanId { get; set; }
        public string Description { get; set; }
    }
}
