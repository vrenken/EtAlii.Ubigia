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

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
        Task<IReadOnlyEntry> AssignNode(Identifier location, INode node, ExecutionScope scope);

        // TODO: Refactor to IObserver<object> output pattern and get rid of the async await constructions.
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
