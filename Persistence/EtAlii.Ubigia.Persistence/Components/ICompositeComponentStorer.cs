namespace EtAlii.Ubigia.Persistence
{
    public interface ICompositeComponentStorer
    {
        void Store(ContainerIdentifier container, CompositeComponent component);
    }
}
