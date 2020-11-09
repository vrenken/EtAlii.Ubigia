﻿namespace EtAlii.xTechnology.Structure.Workflow
{
    public interface ICommandProcessor
    {
        void Process(ICommand command, ICommandHandler handler);
    }
}