namespace EtAlii.Ubigia.Api
{
    using System;
    using System.Linq;

    public class Configuration<TExtension, TConfiguration> : IConfiguration<TExtension, TConfiguration> 
        where TExtension : class 
        where TConfiguration : Configuration<TExtension, TConfiguration>
    {
        public TExtension[] Extensions { get; private set; }

        public Configuration()
        {
            Extensions = new TExtension[0];
        }

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