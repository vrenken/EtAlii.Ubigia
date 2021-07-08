// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Facade that hides away complex logical Node operations.
    /// </summary>
    public interface ILogicalNodeSet
    {
        void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output);

        // Can these 4 Assign method in ILogicalNodeSet be refactored to adhere to the IObserver<object> pattern?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/74
        Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope);
        Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope);
        Task<IReadOnlyEntry> AssignNode(Identifier location, Node node, ExecutionScope scope);
        Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope);

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
