// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class ServiceFactoryBase : IServiceFactory
    {
        /// <inheritdoc />
        public abstract IService Create(
            IConfigurationSection configuration,
            IConfiguration configurationRoot,
            IConfigurationDetails configurationDetails);
    }
}
