namespace EtAlii.Servus.Hosting.TrayIconHost
{
    public interface ITrayIconHostProvider
    {
        ITrayIconHost Host { get; }
        void Initialize(IHost host);
    }
}