namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IGraphSLScriptContext context, string account, UserSettings settings);
    }
}