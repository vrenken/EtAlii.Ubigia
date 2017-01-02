//namespace EtAlii.Servus.Api.Functional
//{
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Threading.Tasks;

//    internal class PathSubjectForVariableAssignmentOperationConverter : IPathSubjectForVariableAssignmentOperationConverter
//    {
//        private readonly IProcessingContext _context;
//        private readonly IPathProcessor _pathProcessor;
//        private readonly IEntriesToDynamicNodesConverter _entriesToDynamicNodesConverter;

//        public PathSubjectForVariableAssignmentOperationConverter(
//            IProcessingContext context, 
//            IPathProcessor pathProcessor, 
//            IEntriesToDynamicNodesConverter entriesToDynamicNodesConverter)
//        {
//            _context = context;
//            _pathProcessor = pathProcessor;
//            _entriesToDynamicNodesConverter = entriesToDynamicNodesConverter;
//        }

//        public async Task<object> Convert(PathSubject pathSubject, ProcessParameters<Subject, SequencePart> parameters, ExecutionScope scope)
//        {
//            var isAbsolute = pathSubject.IsAbsolute;

//            object result = null;
//            var variableName = ((VariableSubject)parameters.FuturePart).Name;
//            ScopeVariable variable;
//            if (_context.Scope.Variables.TryGetValue(variableName, out variable))
//            {
//                if (variable.Value is PathSubject)
//                {
//                    // Ok, let's forward the whole pathsubject.
//                    result = pathSubject;
//                }
//                else if (variable.Value is INode)
//                {
//                    // Ok, let's forward the whole pathsubject.
//                    result = pathSubject;
//                }
//                else
//                {
//                    if (isAbsolute)
//                    {
//                        // Query the database to retrieve the result.
//                        var outputObservable = Observable.Create<object>(async outputObserver =>
//                        {
//                            await _pathProcessor.Process((PathSubject)parameters.Target, scope, outputObserver);
//                            return Disposable.Empty;
//                        });

//                        var entries = (await outputObservable.ToArray())
//                            .Cast<IReadOnlyEntry>()
//                            .ToArray();

//                        result = await _entriesToDynamicNodesConverter.Convert(entries, scope);
//                    }
//                    else
//                    {
//                        result = pathSubject;
//                    }
//                }
//            }
//            else
//            {
//                if (isAbsolute)
//                {
//                    // Query the database to retrieve the result.
//                    var outputObservable = Observable.Create<object>(async outputObserver =>
//                    {
//                        await _pathProcessor.Process((PathSubject)parameters.Target, scope, outputObserver);
//                        return Disposable.Empty;
//                    });

//                    var entries = (await outputObservable.ToArray())
//                        .Cast<IReadOnlyEntry>()
//                        .ToArray();

//                    result = await _entriesToDynamicNodesConverter.Convert(entries, scope);
//                }
//                else
//                {
//                    result = pathSubject;
//                }
//            }
//            return result;
//        }
//    }
//}
