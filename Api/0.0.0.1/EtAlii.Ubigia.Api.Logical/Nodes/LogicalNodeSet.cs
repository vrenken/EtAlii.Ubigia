namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;

    public class LogicalNodeSet : ILogicalNodeSet
    {
        private readonly IFabricContext _fabric;
        private readonly IGraphPathTraverserFactory _graphPathTraverserFactory;
        private readonly IGraphComposerFactory _graphComposerFactory;
        private readonly IGraphPathAssignerFactory _graphPathAssignerFactory;

        public LogicalNodeSet(
            IFabricContext fabric,
            IGraphPathTraverserFactory graphPathTraverserFactory, 
            IGraphComposerFactory graphComposerFactory, 
            IGraphPathAssignerFactory graphPathAssignerFactory)
        {
            _fabric = fabric;
            _graphPathTraverserFactory = graphPathTraverserFactory;
            _graphComposerFactory = graphComposerFactory;
            _graphPathAssignerFactory = graphPathAssignerFactory;
        }

        public async Task<IReadOnlyEntry> Select(GraphPath path, ExecutionScope scope)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_fabric);
            var traverser = _graphPathTraverserFactory.Create(configuration);
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<IReadOnlyEntry>> SelectMany(GraphPath path, ExecutionScope scope)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_fabric);
            var traverser = _graphPathTraverserFactory.Create(configuration);
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            return await results.ToArray();
        }

        public void SelectMany(GraphPath path, ExecutionScope scope, IObserver<object> output)
        {
            var configuration = new GraphPathTraverserConfiguration()
                .Use(_fabric);
            var traverser = _graphPathTraverserFactory.Create(configuration);
            traverser.Traverse(path, Traversal.BreadthFirst, scope, output);
        }

        // TODO: The Assignment result should be a IReadOnlyEntry and not an INode.
        public async Task<INode> Assign(Identifier location, object item, ExecutionScope scope)
        {
            var assigner = _graphPathAssignerFactory.Create(_fabric);
            return await assigner.Assign(location, item, scope);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, string child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Add(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Add(Identifier parent, Identifier child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Add(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Remove(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Remove(parent, child, scope);
        }

        public async Task<IReadOnlyEntry> Link(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Link(location, itemName, item, scope);
        }

        public async Task<IReadOnlyEntry> Unlink(Identifier location, string itemName, Identifier item, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Unlink(location, itemName, item, scope);
        }

        public async Task<IEditableEntry> Prepare()
        {
            return await _fabric.Entries.Prepare();
        }

        public async Task<IReadOnlyEntry> Change(IEditableEntry entry, ExecutionScope scope)
        {
            return await _fabric.Entries.Change(entry, scope);
        }

        public async Task<IReadOnlyEntry> Rename(Identifier item, string newName, ExecutionScope scope)
        {
            var composer = _graphComposerFactory.Create(_fabric);
            return await composer.Rename(item, newName, scope);
        }
    }
}