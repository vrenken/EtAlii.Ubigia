namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IBlobStorer
    {
        void Store(ContainerIdentifier container, IBlob blob);
    }
}
