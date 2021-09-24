﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Transport
{
    using EtAlii.xTechnology.Hosting;

    public class InfrastructureServiceFactory : INewServiceFactory
    {
        public INewService Create(ServiceConfiguration configuration) => new InfrastructureService(configuration, null);
    }
}
