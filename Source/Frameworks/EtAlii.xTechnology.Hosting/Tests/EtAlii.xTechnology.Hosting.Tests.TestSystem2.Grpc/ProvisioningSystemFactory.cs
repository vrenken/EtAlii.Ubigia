// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Grpc
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class ProvisioningSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration, IConfigurationRoot configurationRoot, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<ISystem, ProvisioningSystem>();
            container.Register<ISystemCommandsFactory, ProvisioningSystemCommandsFactory>();

            return container.GetInstance<ISystem>();
        }
    }
}
