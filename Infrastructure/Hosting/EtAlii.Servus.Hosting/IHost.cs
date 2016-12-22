namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.Servus.Infrastructure;
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Storage;

    public interface IHost
    {
        IHostConfiguration Configuration { get; }
        IInfrastructure Infrastructure { get; }
        IStorage Storage { get; }

        void Start();
        void Stop();
    }
}