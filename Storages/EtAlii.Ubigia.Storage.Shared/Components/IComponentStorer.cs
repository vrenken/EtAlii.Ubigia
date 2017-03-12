namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IComponentStorer
    {
        void Store<T>(ContainerIdentifier container, T component)
            where T : class, IComponent;
    }
}
