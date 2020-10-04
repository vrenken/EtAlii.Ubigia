namespace EtAlii.Ubigia.Api.Logical
{
    using System.Threading.Tasks;

    public interface IGraphUpdater
    {
        Task<IEditableEntry> Update(IReadOnlyEntry entry, ExecutionScope scope);
        Task<IEditableEntry> Update(IReadOnlyEntry entry, string newType, ExecutionScope scope);
    }
}