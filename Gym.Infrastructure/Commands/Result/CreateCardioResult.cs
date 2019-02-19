using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class CreateCardioResult : ICommand
    {
        public Guid TrainingDayId { get; protected set; }
        public Guid ExerciseId { get; protected set; }
        public int Distance { get; set; }
        public string Time { get; set; }
    }
}
