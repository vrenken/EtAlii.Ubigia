namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.Ubigia.Api.Functional;

    public interface IProvisioning
    {
        string Status { get; }

        IDataContext Data { get; }
        IProvisioningConfiguration Configuration { get; }
        void Stop();
        void Start();
    }
}
