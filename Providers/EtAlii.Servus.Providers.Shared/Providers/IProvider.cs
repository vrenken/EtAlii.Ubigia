namespace EtAlii.Servus.Provisioning
{
    using EtAlii.Servus.Api.Functional;

    public interface IProvider
    {
        IProviderConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
