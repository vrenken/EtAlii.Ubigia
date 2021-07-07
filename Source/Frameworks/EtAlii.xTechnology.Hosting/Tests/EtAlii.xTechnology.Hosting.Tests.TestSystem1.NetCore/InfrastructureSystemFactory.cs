// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.NetCore
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration, IConfigurationRoot configurationRoot, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<ISystem, InfrastructureSystem>();
            container.Register<ISystemCommandsFactory, InfrastructureSystemCommandsFactory>();

            return container.GetInstance<ISystem>();
        }
    }
}
