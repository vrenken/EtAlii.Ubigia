// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical;

using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

public sealed class GraphPathTraverser : IGraphPathTraverser
{
    private readonly IPathTraversalContext _pathTraversalContext;
    private readonly IBreadthFirstTraversalAlgorithm _breadthFirstTraversalAlgorithm;
    private readonly IDepthFirstTraversalAlgorithm _depthFirstTraversalAlgorithm;
    private readonly ITemporalGraphPathWeaver _temporalGraphPathWeaver;

    public GraphPathTraverser(
        IPathTraversalContext pathTraversalContext,
        IBreadthFirstTraversalAlgorithm breadthFirstTraversalAlgorithm,
        IDepthFirstTraversalAlgorithm depthFirstTraversalAlgorithm,
        ITemporalGraphPathWeaver temporalGraphPathWeaver)
    {
        _pathTraversalContext = pathTraversalContext;
        _breadthFirstTraversalAlgorithm = breadthFirstTraversalAlgorithm;
        _depthFirstTraversalAlgorithm = depthFirstTraversalAlgorithm;
        _temporalGraphPathWeaver = temporalGraphPathWeaver;
    }

    public async Task<IReadOnlyEntry> TraverseToSingle(Identifier identifier, ExecutionScope scope, bool traverseToFinal = true)
    {
        var results = Observable.Create<IReadOnlyEntry>(output =>
        {
            Traverse(GraphPath.Create(identifier), Traversal.DepthFirst, scope, output, traverseToFinal);
            return Disposable.Empty;
        }).ToHotObservable();
        return await results.SingleAsync();
    }

    public void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true)
    {
        if (traverseToFinal)
        {
            path = _temporalGraphPathWeaver.Weave(path);
        }

        var innerObservable = Observable.Create<Identifier>(async innerObserver =>
        {
            var results = traversal switch
            {
                Traversal.DepthFirst => _depthFirstTraversalAlgorithm
                    .Traverse(path, Identifier.Empty, _pathTraversalContext, scope)
                    .ConfigureAwait(false),
                Traversal.BreadthFirst => _breadthFirstTraversalAlgorithm
                    .Traverse(path, Identifier.Empty, _pathTraversalContext, scope)
                    .ConfigureAwait(false),
                _ => throw new InvalidOperationException($"Traversal request not understood: {traversal}")
            };

            await foreach (var result in results)
            {
                innerObserver.OnNext(result);
            }

            innerObserver.OnCompleted();

            return Disposable.Empty;
        });

        // we do not want a cold observable. This should work out of the box as well.
        //innerObservable = ToHotObservable(innerObservable)

        innerObservable.Distinct().SubscribeAsync(
            onError: output.OnError,
            onCompleted: output.OnCompleted,
            onNext: async o =>
            {
                var entry = await _pathTraversalContext.Entries
                    .Get(o, scope)
                    .ConfigureAwait(false);
                output.OnNext(entry);
            });
    }
}
