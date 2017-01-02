//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;

//    internal class ScriptProcessor3 : IScriptProcessor2, IScriptProcessor
//    {
//        private readonly ISequenceProcessor _sequenceProcessor;
//        private readonly IProcessingContext _context;
//        public ScriptProcessor3(
//            ISequenceProcessor sequenceProcessor,
//            IProcessingContext context)
//        {
//            _sequenceProcessor = sequenceProcessor;
//            _context = context;
//        }

//        public IObservable<SequenceProcessingResult> Process(Script script)
//        {
//            var observableScriptOutput = CreateObservableScriptResult(script);

//            // We want all subscriptions to have access to all results.
//            observableScriptOutput = ToHotObservable(observableScriptOutput);

//            return observableScriptOutput;
//        }

//        private IObservable<SequenceProcessingResult> CreateObservableScriptResult(Script script)
//        {
//            var observableScriptOutput = Observable.Create<SequenceProcessingResult>(async scriptOutput =>
//            {
//                try
//                {
//                    // We need to create execution plans for all of the sequences.
//                    var sequences = script.Sequences.ToArray();

//                    var totalSequences = sequences.Length;

//                    for (int i = 0; i < totalSequences; i++)
//                    {
//                        var sequence = sequences[i];

//                        var observableSequenceOutput = CreateObservableSequenceOutput(sequence);
//                        //var observableSequenceOutput = executionPlan.Execute();

//                        // We want all subscriptions to have access to all results.
//                        observableSequenceOutput = ToHotObservable(observableSequenceOutput);

//                        var sequenceResult = new SequenceProcessingResult(sequence, null, i, totalSequences,
//                            observableSequenceOutput);
//                        scriptOutput.OnNext(sequenceResult);

//                        // We need to halt execution of the next sequence until the current one has finished.
//                        await observableSequenceOutput.LastOrDefaultAsync();
//                    }

//                    // After iterating through the sequences script observation has ended. Please keep in mind 
//                    // this is not the same for all sequence observables. The last one could still be receiving results. 
//                    scriptOutput.OnCompleted();
//                }
//                catch (AggregateException aggregateException)
//                {
//                    Exception e = aggregateException;
//                    do
//                    {
//                        e = e.InnerException;
//                    }
//                    while (e is AggregateException);

//                    // An exception on this level should be propagated to the script output observer.
//                    scriptOutput.OnError(e);
//                }
//                catch (Exception e)
//                {
//                    // An exception on this level should be propagated to the script output observer.
//                    scriptOutput.OnError(e);
//                }

//                return Disposable.Empty;
//            });
//            return observableScriptOutput;
//        }

//        private IObservable<object> CreateObservableSequenceOutput(Sequence sequence)
//        {
//            var observableSequenceOutput = Observable.Create<object>(async sequenceOutput =>
//            {
//                try
//                {
//                    await _sequenceProcessor.Process(sequence, sequenceOutput);

//                    // We complete the output observer in this class. Might be less complex than in another class.
//                    sequenceOutput.OnCompleted();
//                }
//                catch (Exception e)
//                {
//                    // An exception on this level should be propagated to the sequence observer.
//                    sequenceOutput.OnError(e);
//                }

//                return Disposable.Empty;
//            });
//            return observableSequenceOutput;
//        }

//        private IObservable<T> ToHotObservable<T>(IObservable<T> observable)
//        {
//            var hotObservable = observable.Replay();
//            hotObservable.Connect();
//            return hotObservable.AsObservable();
//        }
//    }
//}
