using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class UpdateWeightResult : ICommand
    {
        public Guid Id { get; set; }
        public int Series { get;  set; }
        public float Weight { get;  set; }
        public int Reps { get;  set; }
    }
}
