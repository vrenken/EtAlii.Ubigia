namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IBlobStorer
    {
        void Store(ContainerIdentifier container, IBlob blob);
    }
}
