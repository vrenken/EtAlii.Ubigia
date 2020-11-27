namespace EtAlii.Ubigia.Api.Fabric
{
    public interface IEntryCacheHelper
    {
        IReadOnlyEntry Get(in Identifier identifier);

        void Store(IReadOnlyEntry entry);

        bool ShouldStore(IReadOnlyEntry entry);

        void InvalidateRelated(IReadOnlyEntry entry);
    }
}