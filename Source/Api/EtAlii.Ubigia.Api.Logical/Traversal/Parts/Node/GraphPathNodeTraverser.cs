// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Collections.Generic;

internal sealed class GraphPathNodeTraverser : IGraphPathNodeTraverser
{
    public void Configure(TraversalParameters parameters)
    {
        var name = ((GraphNode)parameters.Part).Name;

        parameters.Input.SubscribeAsync(
            onError: e => parameters.Output.OnError(e),
            onNext: async start =>
            {
                if (start == Identifier.Empty)
                {
                    var root = await parameters.Context.Roots.Get(name, parameters.Scope).ConfigureAwait(false);
                    parameters.Output.OnNext(root.Identifier);
                }
                else
                {
                    var entry = await parameters.Context.Entries.Get(start, parameters.Scope).ConfigureAwait(false);
                    if (entry.Type == name)
                    {
                        parameters.Output.OnNext(entry.Id);
                    }
                }
            },
            onCompleted: () => parameters.Output.OnCompleted());

    }

    public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
    {
        var name = ((GraphNode) part).Name;

        if (start == Identifier.Empty)
        {
            var root = await context.Roots.Get(name, scope).ConfigureAwait(false);
            yield return root.Identifier;
        }
        else
        {
            var entry = await context.Entries.Get(start, scope).ConfigureAwait(false);
            if (entry.Type == name)
            {
                yield return entry.Id;
            }
        }
    }
}
