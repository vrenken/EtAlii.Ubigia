namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class QueryProcessorOld : IQueryProcessorOld
    {
        private readonly IQueryExecutionPlanner _queryExecutionPlanner;
        private readonly IQueryProcessorConfiguration _configuration;

        public QueryProcessorOld(
            IQueryExecutionPlanner queryExecutionPlanner,
            IQueryProcessorConfiguration configuration)
        {
            _queryExecutionPlanner = queryExecutionPlanner;
            _configuration = configuration;
        }

        public IObservable<QueryProcessingResult> Process(Query query)
        {
            var queryOutput = CreateObservableQueryResult(query);

            // We want all subqueryions to have access to all results.
            queryOutput = queryOutput
                //.SubscribeOn(NewThreadScheduler.Default)
                //.ObserveOn(NewThreadScheduler.Default)
                .ToHotObservable();

            return queryOutput;
        }

//        private int CountSteps(IEnumerable<FragmentContext> fragments)
//        {
//            var count = 1;
//            
//            foreach (var fragment in fragments)
//            {
//                count += CountSteps(fragment.Children);
//            }
//
//            return count;
//        }
        
        private IObservable<QueryProcessingResult> CreateObservableQueryResult(Query query)
        {
            var observableQueryOutput = Observable.Create<QueryProcessingResult>(async queryOutput =>
            {
                try
                {
                    // We need to create execution plans for all of the sequences.
                    _queryExecutionPlanner.Plan(query, out var rootFragmentMetadata, out var executionPlans);
                    //var structure = query.Structure;

                    var totalExecutionPlans = executionPlans.Length;//.Select(plan => plan is Stru) CountSteps(rootStructures);

                    for (var executionPlanIndex = 0; executionPlanIndex < totalExecutionPlans; executionPlanIndex++)
                    {
                        //var sequence = sequences[executionPlanIndex];
                        var executionPlan = executionPlans[executionPlanIndex];
                        await ProcessExecutionPlan(query, rootFragmentMetadata, executionPlan, executionPlanIndex, totalExecutionPlans, queryOutput);
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
            Query query,
            FragmentMetadata rootFragmentMetadata, 
            FragmentExecutionPlan executionPlan, 
            int executionPlanIndex, 
            int totalExecutionPlans, 
            IObserver<QueryProcessingResult> queryOutput)
        {
            var executionScope = new QueryExecutionScope();

            var executionPlanOutput = await executionPlan.Execute(executionScope);
            //var observableQueryOutput = Observable.Empty<Structure>();

//            var originalObservableQueryOutput = await executionPlan.Execute(executionScope);
//            var observableQueryOutput = Observable.Empty<Structure>();

            // We want all subqueryions to have access to all results.
            executionPlanOutput = executionPlanOutput
                //.SubscribeOn(CurrentThreadScheduler.Instance)
                //.ObserveOn(CurrentThreadScheduler.Instance)
                .ToHotObservable();

            var sequenceResult = new QueryProcessingResult(query, executionPlan, executionPlanIndex, totalExecutionPlans, executionPlanOutput, new ReadOnlyObservableCollection<Structure>(rootFragmentMetadata.Items));
            queryOutput.OnNext(sequenceResult);

            Exception exception = null;
            var continueEvent = new ManualResetEvent(false);

            // We need to halt execution of the next sequence until the current one has finished.
            executionPlanOutput.Subscribe(
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
