namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using EtAlii.Ubigia.Api.Transport.Management;

    public interface IProviderContext
    {
        IGraphSLScriptContext SystemScriptContext { get; }

        IManagementConnection ManagementConnection { get; }
       
        Task<IGraphSLScriptContext> CreateScriptContext(string accountName, string spaceName);
        Task<IGraphSLScriptContext> CreateScriptContext(Space space);

    }
}