namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;
    using EtAlii.xTechnology.MicroContainer;

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