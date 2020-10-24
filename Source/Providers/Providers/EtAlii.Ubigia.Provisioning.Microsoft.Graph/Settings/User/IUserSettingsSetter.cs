namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface IUserSettingsSetter
    {
        Task Set(IGraphSLScriptContext context, string account, UserSettings settings);
    }
}