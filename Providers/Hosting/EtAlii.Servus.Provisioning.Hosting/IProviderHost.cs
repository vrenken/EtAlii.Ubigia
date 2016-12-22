namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Api.Functional;

    public interface IProviderHost
    {
        IDataContext Data { get; }
        IHostConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
