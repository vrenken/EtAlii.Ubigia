﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport.Admin.Portal
{
    using EtAlii.xTechnology.Hosting;

    public class AdminPortalServiceFactory : INewServiceFactory
    {
        public INewService Create(ServiceConfiguration serviceConfiguration) => new AdminPortalService(serviceConfiguration);
    }
}
