// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class ServiceFactoryBase : IServiceFactory
    {
        public abstract IService Create(IConfigurationSection configuration, IConfigurationDetails configurationDetails);
    }
}