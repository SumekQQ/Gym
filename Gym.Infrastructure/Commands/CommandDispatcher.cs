﻿using Autofac;
using System;

namespace Gym.Infrastructure.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public void Dispatch<T>(T command) where T : ICommand
        {
            if (command == null)
                throw new Exception("Command can not be null");

            var handler = _context.Resolve<ICommandHandler<T>>();
            handler.Handle(command);
        }
    }
}