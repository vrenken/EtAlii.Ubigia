namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface ISystemSettingsSetter
    {
        void Set(IGraphSLScriptContext context, SystemSettings settings);
    }
}