// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public class CommandProcessor : ICommandProcessor
    {
        protected virtual void ProcessCommand(ICommand command, ICommandHandler handler)
        {
            handler.Handle(command);
        }

        public void Process(ICommand command, ICommandHandler handler)
        {
            ProcessCommand(command, handler);
        }

    }
}
