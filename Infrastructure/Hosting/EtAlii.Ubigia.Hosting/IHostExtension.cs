namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using SimpleInjector;

    public interface IHostExtension
    {
        void Initialize(Container container);
    }
}
