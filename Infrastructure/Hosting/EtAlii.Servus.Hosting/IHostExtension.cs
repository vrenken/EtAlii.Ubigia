namespace EtAlii.Servus.Infrastructure.Hosting
{
    using SimpleInjector;

    public interface IHostExtension
    {
        void Initialize(Container container);
    }
}
