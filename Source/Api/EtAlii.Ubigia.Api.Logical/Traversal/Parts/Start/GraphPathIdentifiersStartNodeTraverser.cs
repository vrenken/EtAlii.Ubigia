// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal sealed class GraphPathIdentifiersStartNodeTraverser : IGraphPathIdentifiersStartNodeTraverser
{
    public void Configure(TraversalParameters parameters)
    {
        var identifiers = ((GraphIdentifiersStartNode) parameters.Part).Identifiers;
        parameters.Input.Subscribe(
            onError: e => parameters.Output.OnError(e),
            onNext: _ =>
            {
                foreach (var identifier in identifiers)
                {
                    parameters.Output.OnNext(identifier);
                }
                parameters.Output.OnCompleted();
            },
            onCompleted: () => { });// parameters.Output.OnCompleted()])
    }

    public async IAsyncEnumerable<Identifier> Traverse(GraphPathPart part, Identifier start, IPathTraversalContext context, ExecutionScope scope)
    {
        foreach (var item in ((GraphIdentifiersStartNode)part).Identifiers)
        {
            yield return item;
        }

        await Task.CompletedTask.ConfigureAwait(false);
    }
}
