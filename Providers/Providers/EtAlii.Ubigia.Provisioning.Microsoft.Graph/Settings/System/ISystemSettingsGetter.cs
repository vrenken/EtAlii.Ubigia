namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsGetter
    {
        SystemSettings Get(IGraphSLScriptContext context);
    }
}