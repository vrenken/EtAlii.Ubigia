namespace EtAlii.Ubigia.Provisioning
{
    public interface IProvisioningFactory
    {
        IProvisioningManager Create(IProvisioningConfiguration configuration);
    }
}
