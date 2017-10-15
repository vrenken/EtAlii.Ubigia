namespace EtAlii.Servus.Infrastructure
{
    public interface IConnectionStatusProvider
    {
        bool IsConnected { get; }

        void Initialize(ISystemConnection connection);
    }
}
