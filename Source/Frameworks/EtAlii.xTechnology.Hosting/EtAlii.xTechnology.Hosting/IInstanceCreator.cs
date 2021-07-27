// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public interface IInstanceCreator
    {
        /// <summary>
        /// Try to create a TInstance using the provided configuration section, root, details and name.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configurationRoot"></param>
        /// <param name="configurationDetails"></param>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <typeparam name="TInstance"></typeparam>
        /// <returns></returns>
        bool TryCreate<TInstance>(IConfigurationSection configuration, IConfiguration configurationRoot, IConfigurationDetails configurationDetails, string name, out TInstance instance);

        /// <summary>
        /// Try to create a TInstance using the provided configuration section, root, details and name.
        /// Set throwOnNoFactory to false to ensure no exception gets thrown when no factory is configured
        /// in the provided configuration section.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configurationRoot"></param>
        /// <param name="configurationDetails"></param>
        /// <param name="name"></param>
        /// <param name="instance"></param>
        /// <param name="throwOnNoFactory"></param>
        /// <typeparam name="TInstance"></typeparam>
        /// <returns></returns>
        bool TryCreate<TInstance>(IConfigurationSection configuration, IConfiguration configurationRoot, IConfigurationDetails configurationDetails, string name, out TInstance instance, bool throwOnNoFactory);
    }
}
