//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;

//    internal class ScriptProcessor : IScriptProcessor
//    {
//        private readonly ISequenceProcessor _sequenceProcessor;

//        public ScriptProcessor(
//            ISequenceProcessor sequenceProcessor)
//        {
//            _sequenceProcessor = sequenceProcessor;
//        }

//        public IObservable<SequenceProcessingResult> Process(Script script)
//        {
//            var observableScriptResult = CreateObservableScriptResult(script);

//            // We want all subscriptions to have access to all results.
//            observableScriptResult = ToHotObservable(observableScriptResult);

//            return observableScriptResult;
//        }

//        private IObservable<SequenceProcessingResult> CreateObservableScriptResult(Script script)
//        {
//            var observableScriptResult = Observable.Create<SequenceProcessingResult>(async scriptObserver =>
//            {
//                try
//                {

//                    var sequences = script.Sequences.ToArray();
//                    var totalSequences = sequences.Length;

//                    for (int i = 0; i < totalSequences; i++)
//                    {
//                        var sequence = sequences[i];

//                        var observableSequenceResult = CreateObservableSequenceResult(sequence);

//                        // We want all subscriptions to have access to all results.
//                        observableSequenceResult = ToHotObservable(observableSequenceResult);

//                        var sequenceResult = new SequenceProcessingResult(sequence, null, i, totalSequences, observableSequenceResult);
//                        scriptObserver.OnNext(sequenceResult);

//                        // We need to halt execution of the next sequence until the current one has finished.
//                        await observableSequenceResult.LastOrDefaultAsync();
//                    }

//                    // After iterating through the sequences script observation has ended. Please keep in mind 
//                    // this is not the same for all sequence observables. The last one could still be receiving results. 
//                    scriptObserver.OnCompleted();
//                }
//                catch (Exception e)
//                {
//                    // An exception on this level should be propagated to the script observer.
//                    scriptObserver.OnError(e);
//                }

//                return Disposable.Empty;
//            });
//            return observableScriptResult;
//        }

//        private IObservable<object> CreateObservableSequenceResult(Sequence sequence)
//        {
//            var observableSequenceResult = Observable.Create<object>(async sequenceObserver =>
//            {
//                try
//                {
//                    await _sequenceProcessor.Process(sequence, sequenceObserver);

//                    // We complete the observer in this class. Might be less complex than in another class.
//                    sequenceObserver.OnCompleted();
//                }
//                catch (Exception e)
//                {
//                    // An exception on this level should be propagated to the sequence observer.
//                    sequenceObserver.OnError(e);
//                }

//                return Disposable.Empty;
//            });
//            return observableSequenceResult;
//        }

//        private IObservable<T> ToHotObservable<T>(IObservable<T> observable)
//        {
//            var hotObservable = observable.Replay();
//            hotObservable.Connect();
//            return hotObservable.AsObservable();
//        }
//    }
//}
