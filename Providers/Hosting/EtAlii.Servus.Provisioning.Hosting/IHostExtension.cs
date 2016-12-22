namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IHostExtension
    {
        void Initialize(Container container);
    }
}
