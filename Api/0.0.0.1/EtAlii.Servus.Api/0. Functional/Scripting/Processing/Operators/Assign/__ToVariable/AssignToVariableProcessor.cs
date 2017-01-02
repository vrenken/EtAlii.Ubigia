//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using EtAlii.xTechnology.Collections;
//    using EtAlii.xTechnology.Structure;

//    internal class AssignToVariableProcessor : IAssignToVariableProcessor
//    {
//        private readonly IProcessingContext _context;
//        private readonly IResultConverterSelector _resultConverterSelector;
//        private readonly ISelector<OperatorParameters, Action<OperatorParameters>> _processorSelector;

//        public AssignToVariableProcessor(
//            IProcessingContext context,
//            IResultConverterSelector resultConverterSelector)
//        {
//            _context = context;
//            _resultConverterSelector = resultConverterSelector;
//            _processorSelector = new Selector<OperatorParameters, Action<OperatorParameters>>()
//                //.Register(p => p.RightSubject is PathSubject, AssignFromPath)
//                .Register(p => p.RightSubject is PathSubject && ((PathSubject)p.RightSubject).IsAbsolute == true, AssignFromInput)
//                .Register(p => p.RightSubject is PathSubject && ((PathSubject)p.RightSubject).IsAbsolute == false, AssignAsRelativePath)
//                .Register(p => true, AssignFromInput);
//        }

//        public void Assign(OperatorParameters parameters)
//        {
//            var processor = _processorSelector.Select(parameters);
//            processor(parameters);
//        }

//        private void AssignFromPath(OperatorParameters parameters)
//        {
//            var variableSubject = (VariableSubject)parameters.LeftSubject;

//            var variableName = variableSubject.Name;
//            var source = parameters.RightSubject.ToString();
//            var pathSubject = (PathSubject)parameters.RightSubject;
//            parameters.Output.OnNext(pathSubject);
//            parameters.Output.OnCompleted();

//            var results = new object[] { pathSubject };
//            ProcessResults(parameters, results, variableName, source);

//        }

//        private void AssignAsRelativePath(OperatorParameters parameters)
//        {
//            var variableSubject = (VariableSubject)parameters.LeftSubject;

//            var variableName = variableSubject.Name;
//            var source = parameters.RightSubject != null ? parameters.RightSubject.ToString() : "None";

//            var pathSubject = (PathSubject)parameters.RightSubject;
//            parameters.Output.OnNext(pathSubject);
//            parameters.Output.OnCompleted();

//            var results = new object[] { pathSubject };
//            ProcessResults(parameters, results, variableName, source);

//        }
//        private void AssignFromInput(OperatorParameters parameters)
//        {
//            var variableSubject = (VariableSubject)parameters.LeftSubject;

//            var variableName = variableSubject.Name;
//            var source = parameters.RightSubject != null ? parameters.RightSubject.ToString() : "None";

//            var observable = Observable.Create<object>(observer =>
//            {
//                var rightResult = parameters.RightInput
//                    .ToEnumerable()
//                    .ToArray();

//                if (rightResult.Any())
//                {
//                    var o = rightResult.Multiple() ? rightResult : rightResult[0];
//                    var resultConverter = _resultConverterSelector.Select(o);
//                    resultConverter(o, parameters.Scope, observer);
//                }
//                observer.OnCompleted();

//                return Disposable.Empty;
//            });

//            var results = observable
//                .ToEnumerable()
//                .ToArray();

//            ProcessResults(parameters, results, variableName, source);
//        }

//        private void ProcessResults(OperatorParameters parameters, object[] results, string variableName, string source)
//        {
//            if (results.Multiple())
//            {
//                SetVariable(results, variableName, source);
//            }
//            else
//            {
//                var first = results.SingleOrDefault();
//                SetVariable(first, variableName, source);
//            }

//            foreach (var result in results)
//            {
//                parameters.Output.OnNext(result);
//            }
//            parameters.Output.OnCompleted();
//        }

//        private void SetVariable(object o, string variableName, string source)
//        {
//            if (o != null)
//            {
//                var variable = new ScopeVariable(o, source);
//                _context.Scope.Variables[variableName] = variable;
//            }
//            else
//            {
//                _context.Scope.Variables.Remove(variableName);
//            }
//        }
//    }
//}
