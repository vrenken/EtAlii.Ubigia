namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IProvider
    {
        IProviderConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
