namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobStorer
    {
        void Store(ContainerIdentifier container, IBlob blob);
    }
}
