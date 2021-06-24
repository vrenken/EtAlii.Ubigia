// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public abstract class CommandHandlerBase<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        protected internal abstract void Handle(TCommand command);

        public void Handle(ICommand command)
        {
            Handle((TCommand)command);
        }
    }
}
