namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;

    public interface ISystemSettingsProvider
    {
        SystemSettings SystemSettings { get; }

        Task Update();
    }
}