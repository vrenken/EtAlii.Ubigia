namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    public interface ISystemSettingsProvider
    {
        SystemSettings SystemSettings { get; }

        void Update();
    }
}