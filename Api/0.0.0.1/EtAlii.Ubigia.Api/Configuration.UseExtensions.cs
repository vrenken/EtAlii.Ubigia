
namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is the base class for all configuration classes.
    /// It provides out of the box support for extensions.
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
                throw new ArgumentException(nameof(extensions));
            }

            var editableConfiguration = (IEditableConfiguration) configuration;
            
            editableConfiguration.Extensions = extensions
                .Cast<IExtension>()
                .Concat(editableConfiguration.Extensions)
                .Distinct()
                .ToArray();
            return configuration;
        }
    }
}