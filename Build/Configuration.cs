namespace EtAlii.Ubigia.Pipelines
{
    using System.ComponentModel;
    using Nuke.Common.Tooling;

    [TypeConverter(typeof(TypeConverter<Configuration>))]
#pragma warning disable CA1724 
    public class Configuration : Enumeration
#pragma warning restore CA1724 
    {

        public static readonly Configuration Debug = new Configuration {Value = nameof(Debug)};
        public static readonly Configuration Release = new Configuration {Value = nameof(Release)};

        public static implicit operator string(Configuration configuration)
        {
            return configuration.Value;
        }
    }
}