namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IProvisioning
    {
        IDataContext Data { get; }
        IProvisioningConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
