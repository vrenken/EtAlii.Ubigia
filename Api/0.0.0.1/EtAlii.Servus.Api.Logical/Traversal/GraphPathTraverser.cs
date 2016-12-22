﻿namespace EtAlii.Servus.Api.Logical
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public class GraphPathTraverser : IGraphPathTraverser
    {
        private readonly ITraversalContextFactory _traversalContextFactory;
        private readonly IBreadthFirstTraversalAlgorithm _breadthFirstTraversalAlgorithm;
        private readonly IDepthFirstTraversalAlgorithm _depthFirstTraversalAlgorithm;
        private readonly ITemporalGraphPathWeaver _temporalGraphPathWeaver;

        public GraphPathTraverser(
            ITraversalContextFactory traversalContextFactory,
            IBreadthFirstTraversalAlgorithm breadthFirstTraversalAlgorithm,
            IDepthFirstTraversalAlgorithm depthFirstTraversalAlgorithm,
            ITemporalGraphPathWeaver temporalGraphPathWeaver)
        {
            _traversalContextFactory = traversalContextFactory;
            _breadthFirstTraversalAlgorithm = breadthFirstTraversalAlgorithm;
            _depthFirstTraversalAlgorithm = depthFirstTraversalAlgorithm;
            _temporalGraphPathWeaver = temporalGraphPathWeaver;
        }
        
        public void Traverse(GraphPath path, Traversal traversal, ExecutionScope scope, IObserver<IReadOnlyEntry> output, bool traverseToFinal = true)
        {
            if (traverseToFinal)
            {
                path = _temporalGraphPathWeaver.Weave(path);
            }

            // We want a traversal context per traverse action.
            var context = _traversalContextFactory.Create();

            var innerObservable = Observable.Create<Identifier>(async innerObserver =>
            {
                switch (traversal)
                {
                    case Traversal.DepthFirst:
                        await _depthFirstTraversalAlgorithm.Traverse(path, Identifier.Empty, context, scope, innerObserver);
                        break;
                    case Traversal.BreadthFirst:
                        await _breadthFirstTraversalAlgorithm.Traverse(path, Identifier.Empty, context, scope, innerObserver);
                        break;
                }

                innerObserver.OnCompleted();

                return Disposable.Empty;
            });

            // we do not want a cold observable. This should work out of the box as well.
            //innerObservable = ToHotObservable(innerObservable);

            innerObservable.Distinct().Subscribe(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var task = Task.Run(async () =>
                    {
                        var entry = await context.Entries.Get(o, scope);
                        output.OnNext(entry);
                    });
                    task.Wait();
                });
        }
    }
}
