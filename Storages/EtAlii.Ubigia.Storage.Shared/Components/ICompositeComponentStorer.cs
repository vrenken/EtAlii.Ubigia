namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface ICompositeComponentStorer
    {
        void Store(ContainerIdentifier container, CompositeComponent component);
    }
}
