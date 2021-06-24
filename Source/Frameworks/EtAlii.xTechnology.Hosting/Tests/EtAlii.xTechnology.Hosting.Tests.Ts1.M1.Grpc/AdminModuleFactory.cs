// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Tests.Infrastructure.Admin.Grpc
{
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminModuleFactory : ModuleFactoryBase
    {
        public override IModule Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IModule, AdminModule>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IModule>();
        }
    }
}
