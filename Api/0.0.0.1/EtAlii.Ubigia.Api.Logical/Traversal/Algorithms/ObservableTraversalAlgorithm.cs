namespace EtAlii.Ubigia.Api.Logical
{
    using System;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class ObservableTraversalAlgorithm : IBreadthFirstTraversalAlgorithm, IDepthFirstTraversalAlgorithm
    {
        private readonly IGraphPathPartTraverserSelector _graphPathPartTraverserSelector;

        public ObservableTraversalAlgorithm(IGraphPathPartTraverserSelector graphPathPartTraverserSelector)
        {
            _graphPathPartTraverserSelector = graphPathPartTraverserSelector;
        }
        public Task Traverse(GraphPath graphPath, Identifier current, ITraversalContext context, ExecutionScope scope, IObserver<Identifier> finalOutput)
        {
            return Task.Run(() =>
            {
                var firstInput = Observable
                    .Return(current)
                    .ToHotObservable();
                var input = firstInput;

                for (var i = 0; i < graphPath.Length; i++)
                {
                    var graphPathPart = graphPath[i];
                    var traverser = _graphPathPartTraverserSelector.Select(graphPathPart);

                    var continueEvent = new AutoResetEvent(false);

                    var previousOutput = Observable.Create<Identifier>(output =>
                    {
                        var parameters = new TraversalParameters(graphPathPart, context, scope, output, input);
                        traverser.Configure(parameters);

                        continueEvent.Set();

                        return Disposable.Empty;
                    }).ToHotObservable();

                    continueEvent.WaitOne();

                    input = previousOutput;

                    if (i == graphPath.Length - 1)
                    {
                        previousOutput.Subscribe(finalOutput);
                    }
                }
            });
        }
    }
}