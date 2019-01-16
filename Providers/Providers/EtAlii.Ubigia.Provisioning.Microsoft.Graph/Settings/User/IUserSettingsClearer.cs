namespace EtAlii.Ubigia.Provisioning.Microsoft.Graph
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IUserSettingsClearer
    {
        void Clear(IGraphSLScriptContext context, string account);
    }
}