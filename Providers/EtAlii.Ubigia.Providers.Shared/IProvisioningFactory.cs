namespace EtAlii.Ubigia.Provisioning
{
    public interface IProvisioningFactory
    {
        IProvisioning Create(IProvisioningConfiguration configuration);
    }
}
