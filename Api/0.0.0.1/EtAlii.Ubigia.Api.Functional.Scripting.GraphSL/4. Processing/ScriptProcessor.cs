namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading;
    using EtAlii.Ubigia.Api.Logical;

    internal class ScriptProcessor : IScriptProcessor
    {
        private readonly IScriptExecutionPlanner _scriptExecutionPlanner;
        private readonly IScriptProcessorConfiguration _configuration;

        public ScriptProcessor(
            IScriptExecutionPlanner scriptExecutionPlanner,
            IScriptProcessorConfiguration configuration)
        {
            _scriptExecutionPlanner = scriptExecutionPlanner;
            _configuration = configuration;
        }

        public IObservable<SequenceProcessingResult> Process(Script script)
        {
            var observableScriptOutput = CreateObservableScriptResult(script);

            // We want all subscriptions to have access to all results.
            observableScriptOutput = observableScriptOutput
                //.SubscribeOn(NewThreadScheduler.Default)
                //.ObserveOn(NewThreadScheduler.Default)
                .ToHotObservable();

            return observableScriptOutput;
        }

        private IObservable<SequenceProcessingResult> CreateObservableScriptResult(Script script)
        {
            var observableScriptOutput = Observable.Create<SequenceProcessingResult>(scriptOutput =>
            {
                try
                {
                    // We need to create execution plans for all of the sequences.
                    var executionPlans = _scriptExecutionPlanner.Plan(script);
                    var sequences = script.Sequences.ToArray();

                    var totalExecutionPlans = executionPlans.Length;

                    for (int i = 0; i < totalExecutionPlans; i++)
                    {
                        var sequence = sequences[i];
                        var executionPlan = executionPlans[i];
                        var executionScope = new ExecutionScope(_configuration.CachingEnabled);

                        var originalObservableSequenceOutput = executionPlan.Execute(executionScope);
                        var observableSequenceOutput = Observable.Empty<object>();

                        // We only show output:
                        // - If we have an assign operator at the start of the sequence.
                        // - If the first part of the sequence is a path. 
                        var firstPart = sequence.Parts.FirstOrDefault();
                        if (firstPart is AssignOperator || firstPart is PathSubject)
                        {
                            observableSequenceOutput = originalObservableSequenceOutput;
                            originalObservableSequenceOutput = null;
                        }

                        // We want all subscriptions to have access to all results.
                        observableSequenceOutput = observableSequenceOutput
                            //.SubscribeOn(CurrentThreadScheduler.Instance)
                            //.ObserveOn(CurrentThreadScheduler.Instance)
                            .ToHotObservable();

                        var sequenceResult = new SequenceProcessingResult(sequence, executionPlan, i, totalExecutionPlans, observableSequenceOutput);
                        scriptOutput.OnNext(sequenceResult);

                        Exception exception = null;
                        var continueEvent = new ManualResetEvent(false);

                        // We need to halt execution of the next sequence until the current one has finished.
                        observableSequenceOutput.Subscribe(o => { }, e => { exception = e; continueEvent.Set(); }, () => { continueEvent.Set(); });
                        continueEvent.WaitOne();
                        if (exception != null)
                        {
                            throw exception;
                        }

                        if (originalObservableSequenceOutput != null)
                        {
                            // But also if we don't attach the original observable sequence output.
                            continueEvent.Reset();
                            originalObservableSequenceOutput.Subscribe(o => { }, e => { exception = e; continueEvent.Set(); }, () => { continueEvent.Set(); });
                            continueEvent.WaitOne();
                            if (exception != null)
                            {
                                throw exception;
                            }
                        }
                    }

                    // After iterating through the sequences script observation has ended. Please keep in mind 
                    // this is not the same for all sequence observables. The last one could still be receiving results. 
                    scriptOutput.OnCompleted();
                }
                catch (Exception e)
                {
                    while (e is AggregateException)
                    {
                        e = ((AggregateException)e).InnerException;
                    }

                    // An exception on this level should be propagated to the script output observer.
                    scriptOutput.OnError(e);
                }

                return Disposable.Empty;
            });
            return observableScriptOutput;
        }
    }
}
