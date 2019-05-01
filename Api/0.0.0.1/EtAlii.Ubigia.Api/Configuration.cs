namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is the base class for all configuration classes.
    /// It provides out of the box support for extensions.
    /// </summary>
    /// <typeparam name="TExtension"></typeparam>
    /// <typeparam name="TConfiguration"></typeparam>
    public class Configuration<TExtension, TConfiguration> : IConfiguration<TExtension, TConfiguration> 
        where TExtension : IExtension
        where TConfiguration : Configuration<TExtension, TConfiguration>
    {
        /// <summary>
        /// The extensions added to this configuration.
        /// </summary>
        public TExtension[] Extensions { get; private set; }

        public Configuration()
        {
            Extensions = new TExtension[0];
        }

        /// <summary>
        /// Add a set of extensions to the configuration.
        /// Filtering is applied to make sure each extension is only applied once.
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public TConfiguration Use(TExtension[] extensions)
        {
            if (extensions == null)
            {
                throw new ArgumentException(nameof(extensions));
            }

            Extensions = extensions
                .Concat(Extensions)
                .Distinct()
                .ToArray();
            return (TConfiguration)this;
        }
    }
}