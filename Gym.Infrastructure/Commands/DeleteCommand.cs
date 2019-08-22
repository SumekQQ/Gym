using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Infrastructure.Commands
{
    public class DeleteCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
