namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
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

        public Task<QueryProcessingResult> Process(Query query)
        {
            // We need to create execution plans for all of the sequences.
            var executionPlans = _queryExecutionPlanner.Plan(query);
            var rootMetadata = query.Structure.Metadata;
            var totalExecutionPlans = executionPlans.Length;

            var result = new QueryProcessingResult(query, totalExecutionPlans, new ReadOnlyObservableCollection<Structure>(rootMetadata.Items));

            var observableQueryOutput = Observable.Create<Structure>(async queryOutput =>
            {
                try
                {
                    for (var executionPlanIndex = 0; executionPlanIndex < totalExecutionPlans; executionPlanIndex++)
                    {
                        //var sequence = sequences[executionPlanIndex];
                        var executionPlan = executionPlans[executionPlanIndex];

                        result.Update(executionPlanIndex, executionPlan);
                        await ProcessExecutionPlan(executionPlan, queryOutput);
                    }
                    result.Update(totalExecutionPlans, null);

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

            result.Update(observableQueryOutput);

            return Task.FromResult(result);
        }

        private async Task ProcessExecutionPlan(FragmentExecutionPlan executionPlan, IObserver<Structure> queryOutput)
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

            //queryOutput.OnNext(sequenceResult);

            Exception exception = null;
            var continueEvent = new ManualResetEvent(false);

            // We need to halt execution of the next sequence until the current one has finished.
            executionPlanOutput.Subscribe(
                onNext: o => queryOutput.OnNext(o), 
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
        }
    }
}
