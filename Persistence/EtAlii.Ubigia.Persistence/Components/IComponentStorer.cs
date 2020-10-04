namespace EtAlii.Ubigia.Persistence
{
    public interface IComponentStorer
    {
        void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent;
    }
}
