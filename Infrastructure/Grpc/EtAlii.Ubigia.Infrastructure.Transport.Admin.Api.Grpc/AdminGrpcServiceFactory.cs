﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Grpc
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminGrpcServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails)
        {
            var container = new Container();

            container.Register<IService, AdminGrpcService>();

            container.Register(() => configuration);
            container.Register(() => configurationDetails);

            return container.GetInstance<IService>();
        }
    }
}
