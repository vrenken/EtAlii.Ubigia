namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IProvisioningExtension
    {
        void Initialize(Container container);
    }
}
