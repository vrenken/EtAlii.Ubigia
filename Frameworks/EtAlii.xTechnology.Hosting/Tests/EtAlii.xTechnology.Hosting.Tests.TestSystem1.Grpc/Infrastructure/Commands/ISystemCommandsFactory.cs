﻿namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Grpc
{
    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}