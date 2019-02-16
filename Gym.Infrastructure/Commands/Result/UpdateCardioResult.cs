using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class UpdateCardioResult : ICommand
    {
        public Guid Id { get; set; }
        public int Distance { get; set; }
        public string Time { get; set; }
    }
}
