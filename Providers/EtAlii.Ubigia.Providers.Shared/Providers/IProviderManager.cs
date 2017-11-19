namespace EtAlii.Ubigia.Provisioning
{
    public interface IProviderManager
    {
        string Status { get; }
        void Start();
        void Stop();
    }
}