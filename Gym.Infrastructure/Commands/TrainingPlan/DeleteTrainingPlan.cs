using System;
using System.Collections.Generic;

namespace Gym.Infrastructure.Commands.TrainingPlan
{
    public class DeleteTrainingPlan : ICommand
    {
        public Guid Id { get; set; }

    }
}
