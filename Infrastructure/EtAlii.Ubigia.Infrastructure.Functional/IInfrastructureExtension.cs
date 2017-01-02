namespace EtAlii.Ubigia.Infrastructure.Functional
{
    using SimpleInjector;

    public interface IInfrastructureExtension
    {
        void Initialize(Container container);
    }
}
