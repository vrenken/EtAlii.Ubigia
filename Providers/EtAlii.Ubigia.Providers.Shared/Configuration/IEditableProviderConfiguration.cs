namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;
    using EtAlii.xTechnology.Logging;

    public interface IEditableProviderConfiguration
    {
        IGraphSLScriptContext SystemScriptContext { get; set; }
    
        IManagementConnection ManagementConnection { get; set; }
        
        IProviderFactory Factory { get; set; }
    
        ILogFactory LogFactory { get; set; }
    
        Func<IDataConnection, IGraphSLScriptContext> ScriptContextFactory { get; set; }
    }
}