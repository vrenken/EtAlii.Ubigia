namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface IUserSettingsSetter
    {
        Task Set(IGraphSLScriptContext context, string account, UserSettings settings);
    }
}