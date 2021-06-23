// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    using System;
    using System.Linq;

    /// <summary>
    /// The UseExtensions class provides methods with which configuration specific settings can be configured without losing configuration type.
    /// This comes in very handy during the fluent method chaining involved. 
    /// </summary>
    public static class ConfigurationUseExtensions
    {
        /// <summary>
        /// Add a set of extensions to the configuration.
        /// Filtering is applied to make sure each extension is only applied once.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public static TConfiguration Use<TConfiguration, TExtension>(this TConfiguration configuration, TExtension[] extensions)
            where TConfiguration: IConfiguration
            where TExtension : IExtension
        {
            if (extensions == null)
            {
                throw new ArgumentException("No extensions specified", nameof(extensions));
            }

            var editableConfiguration = (IEditableConfiguration) configuration;
            
            editableConfiguration.Extensions = extensions
                .Cast<IExtension>()
                .Concat(editableConfiguration.Extensions)
                .Distinct()
                .ToArray();
            return configuration;
        }

        /// <summary>
        /// Use the extensions from one configuration in another.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="otherConfiguration"></param>
        /// <typeparam name="TConfiguration"></typeparam>
        /// <returns></returns>
        public static TConfiguration Use<TConfiguration>(this TConfiguration configuration, ConfigurationBase otherConfiguration)
            where TConfiguration : ConfigurationBase
        {
            var editableConfiguration = (IEditableConfiguration) configuration;

            editableConfiguration.Extensions = ((IEditableConfiguration)otherConfiguration).Extensions;
            
            return configuration;
        }
    }
}