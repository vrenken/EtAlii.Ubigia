// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public abstract class ModuleFactoryBase : IModuleFactory
    {
        /// <inheritdoc />
        public abstract IModule Create(
            IConfigurationSection configuration,
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails);
    }
}
