namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class QueryProcessor : IQueryProcessor
    {
        private readonly IQueryExecutionPlanner _queryExecutionPlanner;
        private readonly IQueryProcessorConfiguration _configuration;

        public QueryProcessor(
            IQueryExecutionPlanner queryExecutionPlanner,
            IQueryProcessorConfiguration configuration)
        {
            _queryExecutionPlanner = queryExecutionPlanner;
            _configuration = configuration;
        }

        public IObservable<QueryProcessingResult> Process(Query query)
        {
            var observableQueryOutput = CreateObservableQueryResult(query);

            // We want all subqueryions to have access to all results.
            observableQueryOutput = observableQueryOutput
                //.SubscribeOn(NewThreadScheduler.Default)
                //.ObserveOn(NewThreadScheduler.Default)
                .ToHotObservable();

            return observableQueryOutput;
        }

        private int CountSteps(IEnumerable<Structure> structures)
        {
            var count = 1;
            
            foreach (var structure in structures)
            {
                count += CountSteps(structure.Children);
            }

            return count;
        }
        
        private IObservable<QueryProcessingResult> CreateObservableQueryResult(Query query)
        {
            var observableQueryOutput = Observable.Create<QueryProcessingResult>(async queryOutput =>
            {
                try
                {
                    // We need to create execution plans for all of the sequences.
                    var executionPlans = _queryExecutionPlanner.Plan(query, out var rootStructures);
                    //var structure = query.Structure;

                    var totalExecutionPlans = CountSteps(rootStructures);

                    for (var executionPlanIndex = 0; executionPlanIndex < totalExecutionPlans; executionPlanIndex++)
                    {
                        //var sequence = sequences[executionPlanIndex];
                        var executionPlan = executionPlans[executionPlanIndex];
                        await ProcessExecutionPlan(rootStructures, executionPlan, executionPlanIndex, totalExecutionPlans, queryOutput);
                    }

                    // After iterating through the fragment query observation has ended. Please keep in mind 
                    // this is not the same for all sequence observables. The last one could still be receiving results. 
                    queryOutput.OnCompleted();
                }
                catch (Exception e)
                {
                    while (e is AggregateException aggregateException)
                    {
                        e = aggregateException.InnerException;
                    }

                    // An exception on this level should be propagated to the query output observer.
                    queryOutput.OnError(e);
                }

                return Disposable.Empty;
            });
            return observableQueryOutput;
        }

        private async Task ProcessExecutionPlan(
            ObservableCollection<Structure> rootStructures,
            FragmentExecutionPlan executionPlan, 
            int executionPlanIndex, 
            int totalExecutionPlans, 
            IObserver<QueryProcessingResult> queryOutput)
        {
            Query query = null;
            var executionScope = new QueryExecutionScope();

            var observableQueryOutput = await executionPlan.Execute(executionScope);
            //var observableQueryOutput = Observable.Empty<Structure>();

//            var originalObservableQueryOutput = await executionPlan.Execute(executionScope);
//            var observableQueryOutput = Observable.Empty<Structure>();

            // We want all subqueryions to have access to all results.
            observableQueryOutput = observableQueryOutput
                //.SubscribeOn(CurrentThreadScheduler.Instance)
                //.ObserveOn(CurrentThreadScheduler.Instance)
                .ToHotObservable();

            var sequenceResult = new QueryProcessingResult(query, executionPlan, executionPlanIndex, totalExecutionPlans, observableQueryOutput, new ReadOnlyObservableCollection<Structure>(rootStructures));
            queryOutput.OnNext(sequenceResult);

            Exception exception = null;
            var continueEvent = new ManualResetEvent(false);

            // We need to halt execution of the next sequence until the current one has finished.
            observableQueryOutput.Subscribe(
                onNext: o => { }, 
                onError: e =>
                {
                    exception = e;
                    continueEvent.Set();
                }, 
                onCompleted: () => { continueEvent.Set(); });
            continueEvent.WaitOne();
            if (exception != null)
            {
                throw exception;
            }

//            if (originalObservableQueryOutput != null)
//            {
//                // But also if we don't attach the original observable sequence output.
//                continueEvent.Reset();
//                originalObservableQueryOutput.Subscribe(
//                    onNext: o => { }, 
//                    onError: e =>
//                    {
//                        exception = e;
//                        continueEvent.Set();
//                    }, 
//                    onCompleted: () => continueEvent.Set());
//                continueEvent.WaitOne();
//                if (exception != null)
//                {
//                    throw exception;
//                }
//            }
        }
    }
}
