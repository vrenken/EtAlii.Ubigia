// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;

    public class LogicalNodeSet : ILogicalNodeSet
    {
        private readonly IGraphComposerFactory _graphComposerFactory;
        private readonly IGraphAssignerFactory _graphAssignerFactory;

        internal IFabricContext Fabric { get; }

        // TODO: Shouldn't these factories just be their instances? e.g. GraphPathTraverser?
        internal IGraphPathTraverserFactory GraphPathTraverserFactory { get; }

        public LogicalNodeSet(
            IFabricContext fabric,
            IGraphPathTraverserFactory graphPathTraverserFactory, 
            IGraphAssignerFactory graphAssignerFactory,
            IGraphComposerFactory graphComposerFactory)
        {
            Fabric = fabric;
            GraphPathTraverserFactory = graphPathTraverserFactory;
            _graphAssignerFactory = graphAssignerFactory;
            _graphComposerFactory = graphComposerFactory;
        }

        public IAsyncEnumerable<IReadOnlyEntry> SelectMany(GraphPath path, ExecutionScope scope)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(Fabric);
            var traverser = GraphPathTraverserFactory.Create(configuration);
            return Observable
                .Create<IReadOnlyEntry>(output =>
                {
                    traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                    return Disposable.Empty;
                })
                .ToAsyncEnumerable();
        }

        public void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(Fabric);
            var traverser = GraphPathTraverserFactory.Create(configuration);
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
        }

        public async Task<INode> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignProperties(location, properties, scope).ConfigureAwait(false);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignTag(location, tag, scope).ConfigureAwait(false);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignNode(Identifier location, INode node, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignNode(location, node, scope).ConfigureAwait(false);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> AssignDynamic(Identifier location, object o, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignDynamic(location, o, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Add(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Add(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Remove(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Remove(parent, child, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Link(location, itemName, item, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Unlink(location, itemName, item, scope).ConfigureAwait(false);
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await Fabric.Entries.Prepare().ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await Fabric.Entries.Change(entry, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(Fabric);
            return await composer.Rename(item, newName, scope).ConfigureAwait(false);
        }
    }
}