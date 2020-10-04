namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    public static class DataConnectionConfigurationUseDataConnectionProfiling
    {
        public static TDataConnectionConfiguration UseDataConnectionProfiling<TDataConnectionConfiguration>(this TDataConnectionConfiguration configuration)
            where TDataConnectionConfiguration : DataConnectionConfiguration
        {
            configuration.Use(new IDataConnectionExtension[]
            {
                new ProfilingDataConnectionExtension(),
            });

            return configuration;
        }
    }
}