﻿namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Api.AspNetCore
{
    using EtAlii.xTechnology.Hosting;
    using EtAlii.xTechnology.MicroContainer;
    using Microsoft.Extensions.Configuration;

    public class AdminWebApiServiceFactory : ServiceFactoryBase
    {
        public override IService Create(IConfigurationSection configuration)
        {
            var container = new Container();

            container.Register<IService, AdminWebApiService>();

            container.Register<IConfigurationSection>(() => configuration);

            return container.GetInstance<IService>();
        }
    }
}
