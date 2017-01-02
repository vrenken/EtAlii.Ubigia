namespace EtAlii.Ubigia.Storage
{
    using EtAlii.Ubigia.Api;

    public interface IBlobSummaryCalculator
    {
        BlobSummary Calculate<T>(ContainerIdentifier container)
            where T: BlobBase;
    }
}
