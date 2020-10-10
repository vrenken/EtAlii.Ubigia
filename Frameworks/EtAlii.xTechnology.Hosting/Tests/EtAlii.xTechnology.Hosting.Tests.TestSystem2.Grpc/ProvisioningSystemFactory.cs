﻿namespace EtAlii.xTechnology.Hosting.Tests.Provisioning.Grpc
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class ProvisioningSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<ISystem, ProvisioningSystem>();
            container.Register<ISystemCommandsFactory, SystemCommandsFactory>();

            return container.GetInstance<ISystem>();
        }
    }
}
