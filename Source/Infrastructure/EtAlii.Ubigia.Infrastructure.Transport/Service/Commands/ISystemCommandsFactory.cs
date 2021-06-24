// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    public interface ISystemCommandsFactory
    {
        ICommand[] Create(ISystem system);
    }
}