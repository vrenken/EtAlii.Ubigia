namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System;
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;
    using SimpleInjector;

    public interface IHostConfiguration
    {
        IStorage Storage { get; }
        IInfrastructure Infrastructure { get; }
        IHostExtension[] Extensions { get; }

        IHostConfiguration Use(IHostExtension[] extensions);

        IHostConfiguration Use(IStorage storage);
        IHostConfiguration Use(IInfrastructure infrastructure);

        IHost GetHost(Container container);

        IHostConfiguration Use<THost>()
            where THost: class, IHost;
    }
}