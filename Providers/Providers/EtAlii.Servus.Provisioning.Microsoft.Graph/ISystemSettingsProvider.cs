namespace EtAlii.Servus.Provisioning.Microsoft.Graph
{
    public interface ISystemSettingsProvider
    {
        SystemSettings SystemSettings { get; }

        void Update();
    }
}