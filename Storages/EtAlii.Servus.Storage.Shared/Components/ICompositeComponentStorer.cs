namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface ICompositeComponentStorer
    {
        void Store(ContainerIdentifier container, CompositeComponent component);
    }
}
