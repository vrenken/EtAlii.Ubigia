namespace EtAlii.Ubigia.Persistence
{
    public interface IBlobSummaryCalculator
    {
        BlobSummary Calculate<T>(ContainerIdentifier container)
            where T: BlobBase;
    }
}
