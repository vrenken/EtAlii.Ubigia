namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsGetter
    {
        Task<UserSettings[]> Get(IGraphSLScriptContext context);
    }
}