namespace EtAlii.Servus.Provisioning.Google.PeopleApi
{
    public interface ISystemSettingsProvider
    {
        SystemSettings SystemSettings { get; }

        void Update();
    }
}