namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.Ubigia.Infrastructure;
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Storage;

    public interface IHost
    {
        IHostConfiguration Configuration { get; }
        IInfrastructure Infrastructure { get; }
        IStorage Storage { get; }

        void Start();
        void Stop();
    }
}