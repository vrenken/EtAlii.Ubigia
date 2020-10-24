namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;

    public interface ISystemSettingsProvider
    {
        SystemSettings SystemSettings { get; }

        Task Update();
    }
}