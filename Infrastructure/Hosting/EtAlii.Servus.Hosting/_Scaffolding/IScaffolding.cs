namespace EtAlii.Servus.Infrastructure.Hosting
{
    using SimpleInjector;

    public interface IScaffolding
    {
        void Register(Container container);
    }
}
