namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProvidersContext
    {
        IGraphSLScriptContext SystemScriptContext { get; }
        IManagementConnection ManagementConnection { get; }
        IProviderConfiguration[] ProviderConfigurations { get; }

        void Initialize(IProviderConfiguration[] providerConfigurations, Func<IDataConnection, IGraphSLScriptContext> scriptContextFactory);

        IGraphSLScriptContext CreateScriptContext(IDataConnection connection);
    }
}