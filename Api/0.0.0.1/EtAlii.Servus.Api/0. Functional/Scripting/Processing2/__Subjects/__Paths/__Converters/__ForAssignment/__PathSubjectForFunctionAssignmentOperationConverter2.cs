//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class PathSubjectForFunctionAssignmentOperationConverter2 : IPathSubjectForFunctionAssignmentOperationConverter2
//    {
//        private readonly IProcessingContext _context;
//        private readonly IPathProcessor _pathProcessor;
//        private readonly IEntriesToDynamicNodesConverter _entriesToDynamicNodesConverter;

//        public PathSubjectForFunctionAssignmentOperationConverter2(
//            IProcessingContext context, 
//            IPathProcessor pathProcessor, 
//            IEntriesToDynamicNodesConverter entriesToDynamicNodesConverter)
//        {
//            _context = context;
//            _pathProcessor = pathProcessor;
//            _entriesToDynamicNodesConverter = entriesToDynamicNodesConverter;
//        }

//        public void Convert(
//            PathSubject pathSubject, 
//            ProcessParameters<Subject, SequencePart> parameters, 
//            ExecutionScope scope, 
//            IObserver<object> output)
//        {
//            var isAbsolute = pathSubject.IsAbsolute;

//            if (isAbsolute)
//            {
//                // Query the database to retrieve the result.

//                var outputObservable = Observable.Create<object>(async outputObserver =>
//                {
//                    await _pathProcessor.Process((PathSubject)parameters.Target, scope, outputObserver);

//                    return Disposable.Empty;
//                });

//                outputObservable.Subscribe(
//                    onNext: async o =>
//                    {
//                        var entry = await _entriesToDynamicNodesConverter.Convert(new[] { (IReadOnlyEntry)o }, scope);
//                        output.OnNext(entry);
//                    },
//                    onError: output.OnError,
//                    onCompleted: output.OnCompleted);
//            }
//            else
//            {
//                output.OnNext(pathSubject);
//                output.OnCompleted();
//            }
//        }
//    }
//}