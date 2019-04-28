namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalNodeSet : ILogicalNodeSet
    {
        private readonly IGraphComposer _graphComposer;
        private readonly IGraphAssigner _graphAssigner;

        internal IFabricContext Fabric { get; }
        internal IGraphPathTraverser GraphPathTraverser { get; }

        public LogicalNodeSet(
            IFabricContext fabric,
            IGraphPathTraverser graphPathTraverser, 
            IGraphAssigner graphAssigner,
            IGraphComposer graphComposer)
        {
            Fabric = fabric;
            GraphPathTraverser = graphPathTraverser;
            _graphAssigner = graphAssigner;
            _graphComposer = graphComposer;
        }

        public async Task<IEnumerable<IReadOnlyEntry>> SelectMany(GraphPath path, ExecutionScope scope)
        {
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                GraphPathTraverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.ToArray();
        }

        public void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output)
        {
            GraphPathTraverser.Traverse(path, Traversal.BreadthFirst, scope, output);
        }

        public async Task<INode> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            return await _graphAssigner.AssignProperties(location, properties, scope);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            return await _graphAssigner.AssignTag(location, tag, scope);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignNode(Identifier location, INode node, ExecutionScope scope)
        {
            return await _graphAssigner.AssignNode(location, node, scope);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignDynamic(Identifier location, object o, ExecutionScope scope)
        {
            return await _graphAssigner.AssignDynamic(location, o, scope);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            return await _graphComposer.Add(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            return await _graphComposer.Add(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            return await _graphComposer.Remove(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            return await _graphComposer.Remove(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await _graphComposer.Link(location, itemName, item, scope);
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            return await _graphComposer.Unlink(location, itemName, item, scope);
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await Fabric.Entries.Prepare();
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await Fabric.Entries.Change(entry, scope);
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            return await _graphComposer.Rename(item, newName, scope);
        }
    }
}