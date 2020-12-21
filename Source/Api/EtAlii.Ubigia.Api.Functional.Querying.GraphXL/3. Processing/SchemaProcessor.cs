namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class SchemaProcessor : ISchemaProcessor
    {
        private readonly ISchemaExecutionPlanner _schemaExecutionPlanner;
        //private readonly ISchemaProcessorConfiguration _configuration

        public SchemaProcessor(
            ISchemaExecutionPlanner schemaExecutionPlanner
            //ISchemaProcessorConfiguration configuration
            )
        {
            _schemaExecutionPlanner = schemaExecutionPlanner;
            //_configuration = configuration
        }

        public Task<SchemaProcessingResult> Process(Schema schema)
        {
            // We need to create execution plans for all of the sequences.
            var executionPlans = _schemaExecutionPlanner.Plan(schema);
            var rootMetadata = executionPlans?.FirstOrDefault()?.Metadata ?? new FragmentMetadata(null, Array.Empty<FragmentMetadata>());
            var totalExecutionPlans = executionPlans?.Length ?? 0;

            var result = new SchemaProcessingResult(schema, totalExecutionPlans, new ReadOnlyObservableCollection<Structure>(rootMetadata.Items));

            var observableSchemaOutput = Observable.Create<Structure>(async schemaOutput =>
            {
                try
                {
                    var executionScope = new SchemaExecutionScope();

                    for (var executionPlanIndex = 0; executionPlanIndex < totalExecutionPlans; executionPlanIndex++)
                    {
                        //var sequence = sequences[executionPlanIndex]
                        var executionPlan = executionPlans![executionPlanIndex];

                        result.Update(executionPlanIndex, executionPlan);
                        await ProcessExecutionPlan(executionPlan, schemaOutput, executionScope).ConfigureAwait(false);
                    }
                    result.Update(totalExecutionPlans, null);

                    // After iterating through the fragment query observation has ended. Please keep in mind 
                    // this is not the same for all sequence observables. The last one could still be receiving results. 
                    schemaOutput.OnCompleted();
                }
                catch (Exception e)
                {
                    while (e is AggregateException aggregateException)
                    {
                        if (aggregateException.InnerException != null) 
                        {
                            e = aggregateException.InnerException;
                        }
                        else 
                        {
                            break;
                        }
                    }

                    // An exception on this level should be propagated to the query output observer.
                    schemaOutput.OnError(e);
                }

                return Disposable.Empty; 
            }).ToHotObservable();

            result.Update(observableSchemaOutput);

            return Task.FromResult(result);
        }

        private async Task ProcessExecutionPlan(FragmentExecutionPlan executionPlan, IObserver<Structure> schemaOutput, SchemaExecutionScope executionScope)
        {
            var executionPlanOutput = await executionPlan.Execute(executionScope).ConfigureAwait(false);
            //var observableQueryOutput = Observable.Empty<Structure>()

//            var originalObservableQueryOutput = await executionPlan.Execute(executionScope)
//            var observableQueryOutput = Observable.Empty<Structure>()

            // We want all subqueryions to have access to all results.
            executionPlanOutput = executionPlanOutput
                //.SubscribeOn(CurrentThreadScheduler.Instance)
                //.ObserveOn(CurrentThreadScheduler.Instance)
                .ToHotObservable();

            //queryOutput.OnNext(sequenceResult)

            Exception exception = null;
            using var continueEvent = new ManualResetEvent(false);

            // We need to halt execution of the next sequence until the current one has finished.
            // ReSharper disable AccessToDisposedClosure
            executionPlanOutput.Subscribe(
                onNext: schemaOutput.OnNext, 
                onError: e =>
                {
                    exception = e;
                    continueEvent.Set();
                }, 
                onCompleted: () => { continueEvent.Set(); });
            continueEvent.WaitOne();
            // ReSharper disable restore AccessToDisposedClosure

            if (exception != null)
            {
                throw exception;
            }
        }
    }
}
