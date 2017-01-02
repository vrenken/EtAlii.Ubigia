namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IHostExtension
    {
        void Initialize(Container container);
    }
}
