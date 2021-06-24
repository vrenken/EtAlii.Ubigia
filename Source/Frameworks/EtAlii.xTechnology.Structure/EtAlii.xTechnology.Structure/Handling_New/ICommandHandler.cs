// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure
{
    public interface ICommandHandler<TCommand>
    where TCommand : ICommand
    {
        TCommand Create();
        void Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, in TParam>
        where TCommand: ICommand<TParam>
    {
        TCommand Create(TParam parameter);
        void Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, in TParam1, in TParam2>
        where TCommand : ICommand<TParam1, TParam2>
    {
        TCommand Create(TParam1 parameter1, TParam2 parameter2);
        void Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, in TParam1, in TParam2, in TParam3>
        where TCommand : ICommand<TParam1, TParam2, TParam3>
    {
        TCommand Create(TParam1 parameter1, TParam2 parameter2, TParam3 parameter3);
        void Handle(TCommand command);
    }
}
