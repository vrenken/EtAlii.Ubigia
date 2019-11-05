﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.Rest
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminRestServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, AdminRestService>();

            container.Register(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
