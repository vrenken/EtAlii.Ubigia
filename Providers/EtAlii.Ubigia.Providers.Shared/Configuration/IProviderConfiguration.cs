﻿namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public interface IProviderConfiguration
    {
        IGraphSLScriptContext SystemScriptContext { get; }

        IManagementConnection ManagementConnection { get; }

        IProviderFactory Factory { get; }
        ILogFactory LogFactory { get; }

        IGraphSLScriptContext CreateScriptContext(IDataConnection connection);
    }
}