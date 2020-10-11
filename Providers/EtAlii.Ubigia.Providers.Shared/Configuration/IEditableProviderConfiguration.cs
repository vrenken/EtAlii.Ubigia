namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IEditableProviderConfiguration
    {
        IGraphSLScriptContext SystemScriptContext { get; set; }
    
        IManagementConnection ManagementConnection { get; set; }
        
        IProviderFactory Factory { get; set; }
    
        Func<IDataConnection, IGraphSLScriptContext> ScriptContextFactory { get; set; }
    }
}