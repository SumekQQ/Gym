using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class DeleteWeightResult : ICommand
    {
        public Guid Id { get; set; }

        public DeleteWeightResult(Guid id)
        {
            Id = id;
        }
    }
}
