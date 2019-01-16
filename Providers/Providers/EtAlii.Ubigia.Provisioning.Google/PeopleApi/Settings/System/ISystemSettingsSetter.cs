namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsSetter
    {
        void Set(IGraphSLScriptContext context, SystemSettings settings);
    }
}