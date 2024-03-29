// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

public sealed class ObservableTraversalAlgorithm : IBreadthFirstTraversalAlgorithm, IDepthFirstTraversalAlgorithm
{
    private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

    public ObservableTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
    {
        _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
    }
    public async IAsyncEnumerable<Identifier> Traverse(GraphPath graphPath, Identifier current, IPathTraversalContext context, ExecutionScope scope)
    {
        var firstInput = Observable
            .Return(current)
            .ToHotObservable();
        var input = firstInput;

        // We can reuse the auto reset event as it is used sequentially.
        using var continueEvent = new AutoResetEvent(false);

        for (var i = 0; i < graphPath.Length; i++)
        {
            var graphPathPart = graphPath[i];
            var traverser = _graphPathPartTraverserSelector.Select(graphPathPart);

            continueEvent.Reset();

            var previousOutput = Observable.Create<Identifier>(output =>
            {
                // ReSharper disable once AccessToModifiedClosure
                var parameters = new TraversalParameters(graphPathPart, context, scope, output, input);
                traverser.Configure(parameters);

                // ReSharper disable once AccessToDisposedClosure
                continueEvent.Set();

                return Disposable.Empty;
            }).ToHotObservable();

            continueEvent.WaitOne();

            input = previousOutput;

            if (i == graphPath.Length - 1)
            {
                var results = previousOutput
                    .ToAsyncEnumerable()
                    .ConfigureAwait(false);
                await foreach (var result in results)
                {
                    yield return result;
                }
            }
        }
    }
}
