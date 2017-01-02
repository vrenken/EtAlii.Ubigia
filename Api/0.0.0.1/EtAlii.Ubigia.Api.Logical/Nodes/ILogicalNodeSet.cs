namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Facade that hides away complex logical Node operations.
    /// </summary>
    public interface ILogicalNodeSet
    {
        // TODO: refactor to extension method and move to test projects.
        Task<IReadOnlyEntry> Select(GraphPath path, ExecutionScope scope);

        void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output);

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<INode> Assign(Identifier location, object item, ExecutionScope scope);

        Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope);
        Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope);
        Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope);
        Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope);

        Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope);

        Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope);
        Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope);

        Task<IEditableEntry> Prepare();
        Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope);
    }
}