using Autofac;
using System;
using System.Threading.Tasks;

namespace Gym.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public async Task Dispatch<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new Exception("Command can not be null");

            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.Handle(command);
        }
    }
}
