namespace EtAlii.Servus.Infrastructure.Functional
{
    using SimpleInjector;

    public interface IInfrastructureExtension
    {
        void Initialize(Container container);
    }
}
