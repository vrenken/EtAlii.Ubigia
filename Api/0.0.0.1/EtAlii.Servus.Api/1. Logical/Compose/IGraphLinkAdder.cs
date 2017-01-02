namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    public interface IGraphLinkAdder
    {
        Task<IEditableEntry> AddLink(IEditableEntry updateEntry, IReadOnlyEntry originalLinkEntry, string type, ExecutionScope scope);
        Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(string itemName, IReadOnlyEntry entry, IGraphPathTraverser graphPathTraverser, IReadOnlyEntry result, ExecutionScope scope);
        Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(Identifier item, IReadOnlyEntry entry, IGraphPathTraverser graphPathTraverser, IReadOnlyEntry result, ExecutionScope scope);
    }
}