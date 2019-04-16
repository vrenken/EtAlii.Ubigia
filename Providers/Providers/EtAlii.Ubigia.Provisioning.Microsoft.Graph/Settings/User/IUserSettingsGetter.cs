namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsGetter
    {
        Task<UserSettings[]> Get(IGraphSLScriptContext context);
    }
}