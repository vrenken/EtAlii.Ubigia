// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Fabric;

internal class GraphLinkAdder : IGraphLinkAdder
{
    private readonly IFabricContext _fabric;
    private readonly IGraphChildAdder _graphChildAdder;
    private readonly IGraphPathTraverser _graphPathTraverser;

    public GraphLinkAdder(
        IGraphChildAdder graphChildAdder,
        IGraphPathTraverser graphPathTraverser,
        IFabricContext fabric)
    {
        _graphChildAdder = graphChildAdder;
        _graphPathTraverser = graphPathTraverser;
        _fabric = fabric;
    }

    public async Task<IEditableEntry> AddLink(IEditableEntry updateEntry, IReadOnlyEntry originalLinkEntry, string type, ExecutionScope scope)
    {
        var linkEntry = (IEditableEntry)await _graphChildAdder.AddChild(updateEntry.Id, type, scope).ConfigureAwait(false);
        if (originalLinkEntry != null)
        {
            linkEntry.Downdate = Relation.NewRelation(originalLinkEntry.Id);
            linkEntry = (IEditableEntry)await _fabric.Entries.Change(linkEntry, scope).ConfigureAwait(false);
        }
        return linkEntry;
    }

    public async Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(string itemName, IReadOnlyEntry entry, ExecutionScope scope)
    {
        IReadOnlyEntry result = null;
        var linkEntry = await _fabric.Entries
            .GetRelated(entry.Id, EntryRelations.Child, scope)
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);
        if (linkEntry != null)
        {
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                _graphPathTraverser.Traverse(GraphPath.Create(entry.Id, GraphRelation.Children, new GraphNode(itemName)), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            result = await results.SingleOrDefaultAsync();
        }
        return new Tuple<IReadOnlyEntry, IReadOnlyEntry>(linkEntry, result);
    }

    public async Task<Tuple<IReadOnlyEntry, IReadOnlyEntry>> GetLink(Identifier item, IReadOnlyEntry entry, ExecutionScope scope)
    {
        IReadOnlyEntry result = null;
        var linkEntry = await _fabric.Entries
            .GetRelated(entry.Id, EntryRelations.Child, scope)
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);
        if (linkEntry != null)
        {
            var results = Observable.Create<IReadOnlyEntry>(output =>
            {
                _graphPathTraverser.Traverse(GraphPath.Create(entry.Id, GraphRelation.Children, new GraphWildcard("*")), Traversal.DepthFirst, scope, output);
                return Disposable.Empty;
            }).ToHotObservable();
            result = await results.SingleOrDefaultAsync(e => e.Id == item);
        }
        return new Tuple<IReadOnlyEntry, IReadOnlyEntry>(linkEntry, result);
    }
}
