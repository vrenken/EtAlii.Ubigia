namespace EtAlii.Ubigia.Provisioning
{
    public interface IProvider
    {
        IProviderConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
