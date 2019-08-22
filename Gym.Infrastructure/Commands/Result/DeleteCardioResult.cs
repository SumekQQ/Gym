using System;

namespace Gym.Infrastructure.Commands.Result
{
    public class DeleteCardioResult : ICommand
    {
        public Guid Id { get; set; }

        public DeleteCardioResult(Guid id)
        {
            Id = id;
        }
    }
}
