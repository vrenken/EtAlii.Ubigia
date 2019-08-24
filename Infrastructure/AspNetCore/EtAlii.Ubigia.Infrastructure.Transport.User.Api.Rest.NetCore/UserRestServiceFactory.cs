﻿namespace EtAlii.Ubigia.Infrastructure.Transport.User.Api.Rest.NetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class UserRestServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, UserRestService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
