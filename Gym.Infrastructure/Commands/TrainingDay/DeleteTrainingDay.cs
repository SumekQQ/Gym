using System;

namespace Gym.Infrastructure.Commands.TrainingDay
{
    public class DeleteTrainingDay : ICommand
    {
        public Guid TrainingPlanId { get; set; }
    }
}
