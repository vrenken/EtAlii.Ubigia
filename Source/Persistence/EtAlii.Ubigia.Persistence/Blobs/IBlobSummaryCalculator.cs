namespace EtAlii.Ubigia.Persistence
{
    using System.Threading.Tasks;

    public interface IBlobSummaryCalculator
    {
        Task<BlobSummary> Calculate<T>(ContainerIdentifier container)
            where T: BlobBase;
    }
}
