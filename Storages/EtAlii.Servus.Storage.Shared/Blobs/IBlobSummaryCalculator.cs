namespace EtAlii.Servus.Storage
{
    using EtAlii.Servus.Api;

    public interface IBlobSummaryCalculator
    {
        BlobSummary Calculate<T>(ContainerIdentifier container)
            where T: BlobBase;
    }
}
