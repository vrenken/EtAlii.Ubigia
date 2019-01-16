namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public interface IProviderConfiguration
    {
        IGraphSLScriptContext SystemScriptContext { get; }

        IManagementConnection ManagementConnection { get; }

        IProviderExtension[] Extensions { get; }

        IProviderFactory Factory { get; }
        ILogFactory LogFactory { get; }

        IProviderConfiguration Use(IProviderFactory factory);
        IProviderConfiguration Use(IProviderExtension[] extensions);
        IProviderConfiguration Use(ILogFactory logFactory);
        IProviderConfiguration Use(IManagementConnection managementConnection);
        IProviderConfiguration Use(IGraphSLScriptContext systemScriptContext );

        IProviderConfiguration Use(Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory);
        IGraphSLScriptContext CreateScriptContext(IDataConnection connection);
    }
}