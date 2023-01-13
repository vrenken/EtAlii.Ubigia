// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Threading.Tasks;

internal class GraphRemover : IGraphRemover
{
    private readonly IGraphChildAdder _graphChildAdder;
    private readonly IGraphLinkAdder _graphLinkAdder;
    private readonly IGraphUpdater _graphUpdater;
    private readonly IGraphPathTraverser _graphPathTraverser;

    public GraphRemover(
        IGraphChildAdder graphChildAdder,
        IGraphLinkAdder graphLinkAdder,
        IGraphUpdater graphUpdater,
        IGraphPathTraverser graphPathTraverser)
    {
        _graphChildAdder = graphChildAdder;
        _graphLinkAdder = graphLinkAdder;
        _graphUpdater = graphUpdater;
        _graphPathTraverser = graphPathTraverser;
    }

    public async Task<IReadOnlyEntry> Remove(Identifier parent, string child, ExecutionScope scope)
    {
        // The GraphComposer cannot handle multiple updates yet.
        var entry = await _graphPathTraverser.TraverseToSingle(parent, scope).ConfigureAwait(false);

        // Let's check if a path already exists.
        var linkAddResult = await _graphLinkAdder.GetLink(child, entry, scope).ConfigureAwait(false);
        var originalLinkEntry = linkAddResult.Item1;
        var result = linkAddResult.Item2;
        if (result == null)
        {
            var message = $"Unable to remove item: '{child}' (Not found)";
            throw new GraphComposeException(message);
        }

        var updateEntry = await _graphUpdater.Update(entry, scope).ConfigureAwait(false);
        var updateLinkEntry = await _graphLinkAdder.AddLink(updateEntry, originalLinkEntry, EntryType.Remove, scope).ConfigureAwait(false);
        result = await _graphChildAdder.AddChild(updateLinkEntry.Id, result.Id, scope).ConfigureAwait(false);

        return result;
    }

    public async Task<IReadOnlyEntry> Remove(Identifier parent, Identifier child, ExecutionScope scope)
    {
        // The GraphComposer cannot handle multiple updates yet.
        var entry = await _graphPathTraverser.TraverseToSingle(parent, scope).ConfigureAwait(false);

        // Let's check if a path already exists.
        var linkAddResult = await _graphLinkAdder.GetLink(child, entry, scope).ConfigureAwait(false);
        var originalLinkEntry = linkAddResult.Item1;
        var result = linkAddResult.Item2;
        if (result == null)
        {
            var message = $"Unable to remove item: '{child}' (Not found)";
            throw new GraphComposeException(message);
        }
        var updateEntry = await _graphUpdater.Update(entry, scope).ConfigureAwait(false);
        var updateLinkEntry = await _graphLinkAdder.AddLink(updateEntry, originalLinkEntry, EntryType.Remove, scope).ConfigureAwait(false);
        result = await _graphChildAdder.AddChild(updateLinkEntry.Id, child, scope).ConfigureAwait(false);

        return result;
    }
}
