using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class CreateWeightResult : ICommand
    {
        public Guid TrainingDayId { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public int Series { get; protected set; }
        public float Weight { get; protected set; }
        public int Reps { get; protected set; }
    }
}
