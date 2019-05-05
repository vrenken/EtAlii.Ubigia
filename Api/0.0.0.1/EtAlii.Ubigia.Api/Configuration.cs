namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is the base class for all configuration classes.
    /// It provides out of the box support for extensions.
    /// </summary>
    public abstract class Configuration<TConfiguration> : IConfiguration<TConfiguration> 
        where TConfiguration: Configuration<TConfiguration>
    {
        /// <summary>
        /// The extensions added to this configuration.
        /// </summary>
        protected IExtension[] Extensions { get; private set; }
        
        protected Configuration()
        {
            Extensions = Array.Empty<IExtension>();
        }

        public TExtension[] GetExtensions<TExtension>()
            where TExtension : IExtension
        {
            return Extensions.OfType<TExtension>().ToArray();
        }


        /// <summary>
        /// Add a set of extensions to the configuration.
        /// Filtering is applied to make sure each extension is only applied once.
        /// </summary>
        /// <param name="extensions"></param>
        /// <returns></returns>
        public TConfiguration Use(IExtension[] extensions)
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