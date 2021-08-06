// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal.Razor
{
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Hosting;
    using Microsoft.Extensions.Configuration;

    public class AdminPortalFileHostingService : ServiceBase
    {
        public AdminPortalFileHostingService(IConfigurationSection configuration)
            : base(configuration)
        {
        }

        public override Task Start()
        {
            // Handle Start.
            return Task.CompletedTask;
        }

        public override Task Stop()
        {
            // Handle Stop.
            return Task.CompletedTask;
        }
    }
}
