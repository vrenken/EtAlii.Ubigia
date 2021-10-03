// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using EtAlii.xTechnology.Hosting;

    public class AdminPortalServiceFactory : IServiceFactory
    {
        public IService Create(ServiceConfiguration serviceConfiguration, Status status) => new AdminPortalService(serviceConfiguration, status);
    }
}
