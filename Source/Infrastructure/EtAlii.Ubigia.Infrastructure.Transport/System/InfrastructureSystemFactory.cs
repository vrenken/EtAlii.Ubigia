// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration, IConfigurationRoot configurationRoot, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<ISystem, InfrastructureSystem>();
            container.Register<ISystemCommandsFactory, SystemCommandsFactory>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<ISystem>();
        }
    }
}
