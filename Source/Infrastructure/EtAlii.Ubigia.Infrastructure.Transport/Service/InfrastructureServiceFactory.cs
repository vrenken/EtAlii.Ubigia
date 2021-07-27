// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class InfrastructureServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfiguration configurationRoot, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IInfrastructureService, InfrastructureService>();
            container.Register<IServiceDetailsBuilder, ServiceDetailsBuilder>();

            container.Register(() => configurationRoot);
            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IInfrastructureService>();
        }
    }
}
