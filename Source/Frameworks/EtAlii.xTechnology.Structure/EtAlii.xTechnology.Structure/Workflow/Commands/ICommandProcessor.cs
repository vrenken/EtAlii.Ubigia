// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface ICommandProcessor
    {
        void Process(ICommand command, ICommandHandler handler);
    }
}