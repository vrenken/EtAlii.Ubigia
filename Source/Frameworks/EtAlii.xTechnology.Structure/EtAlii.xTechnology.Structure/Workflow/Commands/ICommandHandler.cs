// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface ICommandHandler
    {
        void Handle(ICommand command);
    }
}
