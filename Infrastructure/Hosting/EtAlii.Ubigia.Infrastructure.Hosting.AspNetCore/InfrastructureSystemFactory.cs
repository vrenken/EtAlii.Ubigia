﻿namespace EtAlii.Ubigia.Infrastructure.Hosting.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;
    using EtAlii.xTechnology.MicroContainer;

    public class InfrastructureSystemFactory : SystemFactoryBase
    {
        public override ISystem Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<ISystem, InfrastructureSystem>();
            container.Register<ISystemCommandsFactory, SystemCommandsFactory>();

            return container.GetInstance<ISystem>();
        }
    }
}
