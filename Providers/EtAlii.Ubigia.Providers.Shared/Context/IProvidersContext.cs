namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProvidersContext
    {
        IGraphSLScriptContext SystemScriptContext { get; }
        IManagementConnection ManagementConnection { get; }
        ProviderConfiguration[] ProviderConfigurations { get; }

        void Initialize(ProviderConfiguration[] providerConfigurations, Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory);

        IGraphSLScriptContext CreateScriptContext(IDataConnection connection);
    }
}