namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsGetter
    {
        Task<SystemSettings> Get(IGraphSLScriptContext context);
    }
}