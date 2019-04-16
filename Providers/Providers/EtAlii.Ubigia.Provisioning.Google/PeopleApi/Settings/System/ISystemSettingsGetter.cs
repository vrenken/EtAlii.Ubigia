namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsGetter
    {
        Task<SystemSettings> Get(IGraphSLScriptContext context);
    }
}