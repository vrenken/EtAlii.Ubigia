namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsGetter
    {
        UserSettings[] Get(IGraphSLScriptContext context);
    }
}