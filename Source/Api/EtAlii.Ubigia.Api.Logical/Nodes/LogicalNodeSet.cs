// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using Microsoft.Extensions.Configuration;

    public class LogicalNodeSet : ILogicalNodeSet
    {
        private readonly IGraphComposerFactory _graphComposerFactory;
        private readonly IConfigurationRoot _configurationRoot;
        private readonly IGraphAssignerFactory _graphAssignerFactory;

        internal IFabricContext Fabric { get; }

        // Shouldn't these factories just be their instances? e.g. GraphPathTraverser?
        // More details can be found in the Github issue below:
        // https://github.com/vrenken/EtAlii.Ubigia/issues/71
        internal IGraphPathTraverserFactory GraphPathTraverserFactory { get; }

        public LogicalNodeSet(
            IConfigurationRoot configurationRoot,
            IFabricContext fabric,
            IGraphPathTraverserFactory graphPathTraverserFactory,
            IGraphAssignerFactory graphAssignerFactory,
            IGraphComposerFactory graphComposerFactory)
        {
            Fabric = fabric;
            GraphPathTraverserFactory = graphPathTraverserFactory;
            _configurationRoot = configurationRoot;
            _graphAssignerFactory = graphAssignerFactory;
            _graphComposerFactory = graphComposerFactory;
        }

        public IAsyncEnumerable<IReadOnlyEntry> SelectMany(GraphPath path, ExecutionScope scope)
        {
            var options = new GraphPathTraverserOptions(_configurationRoot)
                .Use(Fabric);
            var traverser = GraphPathTraverserFactory.Create(options);
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
            var options = new GraphPathTraverserOptions(_configurationRoot)
                .Use(Fabric);
            var traverser = GraphPathTraverserFactory.Create(options);
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
        }

        public async Task<IReadOnlyEntry> SelectSingle(GraphPath path, ExecutionScope scope)
        {
            var options = new GraphPathTraverserOptions(_configurationRoot)
                .Use(Fabric);
            var traverser = GraphPathTraverserFactory.Create(options);
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyEntry> AssignProperties(Identifier location, IPropertyDictionary properties, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignProperties(location, properties, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignTag(Identifier location, string tag, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignTag(location, tag, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignNode(Identifier location, Node node, ExecutionScope scope)
        {
            var assigner = _graphAssignerFactory.Create(Fabric);
            return await assigner.AssignNode(location, node, scope).ConfigureAwait(false);
        }

        public async Task<IReadOnlyEntry> AssignDynamic(Identifier location, object o, ExecutionScope scope)
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
