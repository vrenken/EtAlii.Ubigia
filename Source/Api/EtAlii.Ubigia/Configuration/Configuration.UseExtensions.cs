// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using EtAlii.xTechnology.MicroContainer;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved.
    /// </summary>
    public static class ConfigurationUseExtensions
    {
        /// <summary>
        /// Use the extensions from one configuration in another.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="otherConfiguration"></param>
        /// <typeparam name="TConfiguration"></typeparam>
        /// <returns></returns>
        public static TConfiguration Use<TConfiguration>(this TConfiguration configuration, ConfigurationBase otherConfiguration)
            where TConfiguration : IExtensible
        {
            configuration.Extensions = ((IExtensible)otherConfiguration).Extensions;

            return configuration;
        }
    }
}
