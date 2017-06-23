namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.Ubigia.Api.Transport;

    public static class ProfilingDataConnectionFactoryExtension
    {
        public static IProfilingDataConnection CreateForProfiling(this DataConnectionFactory dataConnectionFactory, IDataConnectionConfiguration configuration)
        {
            configuration.Use(new IDataConnectionExtension[]
            {
                new ProfilingDataConnectionExtension(),
            });

            return (IProfilingDataConnection)dataConnectionFactory.Create(configuration);
        }
    }
}