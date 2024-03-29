﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Portal.Admin;

using EtAlii.xTechnology.Hosting;

public class AdminPortalServiceFactory : IServiceFactory
{
    public IService Create(ServiceConfiguration configuration) => new AdminPortalService(configuration);
}
