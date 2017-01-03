namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public interface IHostExtension
    {
        void Initialize(Container container);
    }
}
