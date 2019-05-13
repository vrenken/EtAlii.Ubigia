namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public interface IProvisioning
    {
        string Status { get; }

        IGraphSLScriptContext Data { get; }
        IProvisioningConfiguration Configuration { get; }
        Task Stop();
        Task Start();
    }
}
