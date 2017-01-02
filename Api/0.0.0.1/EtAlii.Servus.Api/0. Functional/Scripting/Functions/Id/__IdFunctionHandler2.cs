//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Reflection;
//    using System.Runtime.InteropServices.WindowsRuntime;
//    using System.Threading.Tasks;
//    using EtAlii.xTechnology.Structure;

//    internal class IdFunctionHandler2 : IFunctionHandler
//    {
//        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
//        private readonly ParameterSet[] _parameterSets;

//        public string Name { get { return _name; } }
//        private readonly string _name;

//        private readonly ISelector<ArgumentSet, Action<IFunctionContext, ArgumentSet, ExecutionScope, IObserver<object>>> _argumentConverterSelector;
//        private readonly ISelector<object, Action<IFunctionContext, object, ExecutionScope, IObserver<object>>> _inputConverterSelector;

//        private readonly TypeInfo _identifierTypeInfo;
//        private readonly TypeInfo _internalNodeTypeInfo;
//        private readonly TypeInfo _enumerableInternalNodeTypeInfo;
//        private readonly TypeInfo _enumerableIdentifierTypeInfo;

//        public IdFunctionHandler2()
//        {
//            _identifierTypeInfo = typeof(Identifier).GetTypeInfo();
//            _internalNodeTypeInfo = typeof(IInternalNode).GetTypeInfo();
//            _enumerableInternalNodeTypeInfo = typeof(IEnumerable<IInternalNode>).GetTypeInfo();
//            _enumerableIdentifierTypeInfo = typeof(IEnumerable<Identifier>).GetTypeInfo();

//            _parameterSets = new[]
//            {
//                new ParameterSet(true),
//                new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
//                new ParameterSet(false, new Parameter("var", typeof(Identifier))),
//                new ParameterSet(false, new Parameter("var", typeof(IInternalNode))),
//                new ParameterSet(false, new Parameter("var", typeof(IEnumerable<IInternalNode>))),
//                new ParameterSet(false, new Parameter("var", typeof(IEnumerable<Identifier>))),
//            };
//            _name = "Id";

//            _argumentConverterSelector = new Selector<ArgumentSet, Action<IFunctionContext, ArgumentSet, ExecutionScope, IObserver<object>>>()
//                .Register(a => a.Arguments[0] is PathSubject, (c, a, s, o) => ConvertPathToIds2(c, (PathSubject)a.Arguments[0], s, o))
//                .Register(a => _identifierTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s, o) => ConvertPathToIds3(o, (Identifier)a.Arguments[0]))
//                .Register(a => _internalNodeTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s, o) => ConvertPathToIds3(o, ((IInternalNode)a.Arguments[0]).Id))
//                .Register(a => _enumerableIdentifierTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s, o) => ConvertPathToIds4(o, (IEnumerable<Identifier>)a.Arguments[0]))
//                .Register(a => _enumerableInternalNodeTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s, o) => ConvertPathToIds4(o, ((IEnumerable<IInternalNode>)a.Arguments[0]).Select(n => n.Id).AsEnumerable()))
//                .Register(a => a.Arguments[0] == null, (c, a, s, o) => { throw new ScriptProcessingException("No empty argument is allowed for Id function processing"); })
//                .Register(a => true, (c, a, s, o) => { throw new ScriptProcessingException("Unable to convert arguments for Id function processing"); });

//            _inputConverterSelector = new Selector<object, Action<IFunctionContext, object, ExecutionScope, IObserver<object>>>()
//                .Register(i => i is PathSubject, (c, i, s, o) => ConvertPathToIds2(c, (PathSubject)i, s, o))
//                .Register(i => _identifierTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, i, s, o) => ConvertPathToIds3(o, (Identifier)i))
//                .Register(i => _internalNodeTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, i, s, o) => ConvertPathToIds3(o, ((IInternalNode)i).Id))
//                .Register(i => true, (c, i, s, o) => { throw new ScriptProcessingException("Unable to convert input for Id function processing"); });
//        }
        
//        public Task<object> Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, object input, ExecutionScope scope)
//        {
//            throw new NotSupportedException();
//        }

//        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            if (argumentSet.Arguments.Length == 0)
//            {
//                ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
//            }
//            else
//            {
//                ProcessByArgument(context, parameterSet, argumentSet, input, scope, output);
//            }
//        }

//        private void ProcessByArgument(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            var converter = _argumentConverterSelector.Select(argumentSet);
//            converter(context, argumentSet, scope, output);
//        }

//        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            input.Subscribe(
//                onError: (e) => output.OnError(e),
//                onCompleted: () => output.OnCompleted(),
//                onNext: o =>
//                {
//                    var converter = _inputConverterSelector.Select(o);
//                    converter(context, o, scope, output);
//                });
//        }

//        private void ConvertPathToIds3(IObserver<object> output, object o)
//        {
//            output.OnNext(o);
//            output.OnCompleted();
//        }

//        private void ConvertPathToIds4<T>(IObserver<object> output, IEnumerable<T> objects)
//        {
//            foreach (T o in objects)
//            {
//                output.OnNext(o);
//            }
//            output.OnCompleted();
//        }

//        private void ConvertPathToIds2(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope, IObserver<object> output)
//        {
//            var outputObservable = Observable.Create<object>(async outputObserver =>
//            {
//                await context.PathProcessor.Process(pathSubject, scope, outputObserver);

//                return Disposable.Empty;
//            });

//            outputObservable.Subscribe(
//                onError: (e) => output.OnError(e),
//                onCompleted: () => output.OnCompleted(),
//                onNext: o => output.OnNext(((IReadOnlyEntry)o).Id)
//                );
//        }
//    }
//}