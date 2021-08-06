// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public interface IFactory<out TInstance>
    {
        /// <summary>
        /// Create an instance from the provided configuration section.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configurationRoot"></param>
        /// <param name="configurationDetails"></param>
        /// <returns></returns>
        TInstance Create(
            IConfigurationSection configuration,
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails);
    }
}
