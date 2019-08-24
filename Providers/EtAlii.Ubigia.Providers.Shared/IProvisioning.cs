namespace EtAlii.Ubigia.Provisioning
{
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional.Scripting;

    public interface IProvisioning
    {
        string Status { get; }

        IGraphSLScriptContext Data { get; }
        /// <summary>
        /// The Configuration used to instantiate this Context.
        /// </summary>
        IProvisioningConfiguration Configuration { get; }
        Task Stop();
        Task Start();
    }
}
