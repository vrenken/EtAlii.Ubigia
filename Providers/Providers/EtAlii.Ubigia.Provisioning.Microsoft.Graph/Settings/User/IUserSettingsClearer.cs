namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsClearer
    {
        Task Clear(IGraphSLScriptContext context, string account);
    }
}