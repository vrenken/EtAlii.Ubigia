namespace EtAlii.Servus.Provisioning
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IProviderExtension
    {
        void Initialize(Container container);
    }
}
