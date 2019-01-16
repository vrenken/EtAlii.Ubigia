namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProviderContext
    {
        IGraphSLScriptContext SystemScriptContext { get; }

        IManagementConnection ManagementConnection { get; }
       
        IGraphSLScriptContext CreateScriptContext(string accountName, string spaceName);
        IGraphSLScriptContext CreateScriptContext(Space space);

    }
}