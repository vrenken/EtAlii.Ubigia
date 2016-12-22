namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Transport;

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