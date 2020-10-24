namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface ISystemSettingsSetter
    {
        Task Set(IGraphSLScriptContext context, SystemSettings settings);
    }
}