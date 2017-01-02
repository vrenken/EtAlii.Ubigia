namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IProviderHost
    {
        IDataContext Data { get; }
        IHostConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
