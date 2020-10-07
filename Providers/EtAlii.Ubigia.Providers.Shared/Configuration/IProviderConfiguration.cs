namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Diagnostics;

    public interface IProviderConfiguration
    {
        IGraphSLScriptContext SystemScriptContext { get; }

        IManagementConnection ManagementConnection { get; }

        IProviderFactory Factory { get; }
        ILogFactory LogFactory { get; }

        IGraphSLScriptContext CreateScriptContext(IDataConnection connection);
    }
}