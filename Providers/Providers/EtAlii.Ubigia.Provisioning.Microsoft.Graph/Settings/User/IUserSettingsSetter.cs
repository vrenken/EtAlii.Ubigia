namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsSetter
    {
        void Set(IGraphSLScriptContext context, string account, UserSettings settings);
    }
}