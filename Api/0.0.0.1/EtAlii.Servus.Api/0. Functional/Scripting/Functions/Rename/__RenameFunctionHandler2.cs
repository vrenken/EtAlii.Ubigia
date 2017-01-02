//namespace EtAlii.Servus.Api.Functional
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Reactive.Disposables;
//    using System.Reactive.Linq;
//    using System.Reflection;
//    using System.Threading.Tasks;
//    using EtAlii.xTechnology.Collections;
//    using EtAlii.xTechnology.Structure;

//    internal class RenameFunctionHandler2 : IFunctionHandler
//    {
//        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
//        private readonly ParameterSet[] _parameterSets;

//        public string Name { get { return _name; } }
//        private readonly string _name;

//        private readonly ISelector<ArgumentSet, Func<IFunctionContext, ArgumentSet, ExecutionScope, Task<IEnumerable<Identifier>>>> _argumentConverterSelector;
//        private readonly ISelector<ArgumentSet, object, Func<IFunctionContext, ArgumentSet, object, ExecutionScope, Task<IEnumerable<Identifier>>>> _inputConverterSelector;

//        private readonly TypeInfo _identifierTypeInfo;
//        private readonly TypeInfo _internalNodeTypeInfo;
//        private readonly TypeInfo _enumerableInternalNodeTypeInfo;
//        private readonly TypeInfo _enumerableIdentifierTypeInfo;

//        public RenameFunctionHandler2()
//        {
//            _identifierTypeInfo = typeof(Identifier).GetTypeInfo();
//            _internalNodeTypeInfo = typeof(IInternalNode).GetTypeInfo();
//            _enumerableInternalNodeTypeInfo = typeof(IEnumerable<IInternalNode>).GetTypeInfo();
//            _enumerableIdentifierTypeInfo = typeof(IEnumerable<Identifier>).GetTypeInfo();

//            _parameterSets = new []
//            {
//                new ParameterSet(true, new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(string)), new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(PathSubject)), new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(Identifier)), new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(IInternalNode)), new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(IEnumerable<IInternalNode>)), new Parameter("name", typeof(string))),
//                new ParameterSet(false, new Parameter("var", typeof(IEnumerable<Identifier>)), new Parameter("name", typeof(string)))
//            };
//            _name = "Rename";

//            _argumentConverterSelector = new Selector<ArgumentSet, Func<IFunctionContext, ArgumentSet, ExecutionScope, Task<IEnumerable<Identifier>>>>()
//                .Register(a => a.Arguments[1] is string && a.Arguments[0] is PathSubject, (c, a, s) => ConvertPathToIds(c, (PathSubject)a.Arguments[0], s))
//                .Register(a => a.Arguments[1] is string && _identifierTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s) => Task.FromResult<IEnumerable<Identifier>>(new Identifier[] { (Identifier)a.Arguments[0] }))
//                .Register(a => a.Arguments[1] is string && _internalNodeTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s) => Task.FromResult<IEnumerable<Identifier>>(new Identifier[] { ((IInternalNode)a.Arguments[0]).Id }))
//                .Register(a => a.Arguments[1] is string && _enumerableIdentifierTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s) => Task.FromResult((IEnumerable<Identifier>)a.Arguments[0]))
//                .Register(a => a.Arguments[1] is string && _enumerableInternalNodeTypeInfo.IsAssignableFrom(a.ArgumentTypeInfos[0]), (c, a, s) => Task.FromResult(((IEnumerable<IInternalNode>)a.Arguments[0]).Select(n => n.Id).AsEnumerable()))
//                .Register(a => true, (c, a, s) => { throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing"); });

//            _inputConverterSelector = new Selector2<ArgumentSet, object, Func<IFunctionContext, ArgumentSet, object, ExecutionScope, Task<IEnumerable<Identifier>>>>()
//                .Register((a, i) => a.Arguments[0] is string && i is PathSubject, (c, a, i, s) => ConvertPathToIds(c, (PathSubject)i, s))
//                .Register((a, i) => a.Arguments[0] is string && _identifierTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, a, i, s) => Task.FromResult<IEnumerable<Identifier>>(new Identifier[] { (Identifier)i }))
//                .Register((a, i) => a.Arguments[0] is string && _internalNodeTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, a, i, s) => Task.FromResult<IEnumerable<Identifier>>(new Identifier[] { ((IInternalNode)i).Id }))
//                //.Register((a, i) => a.Arguments[0] is string && _enumerableIdentifierTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, a, i, s) => Task.FromResult((IEnumerable<Identifier>)i))
//                //.Register((a, i) => a.Arguments[0] is string && _enumerableInternalNodeTypeInfo.IsAssignableFrom(i.GetType().GetTypeInfo()), (c, a, i, s) => Task.FromResult(((IEnumerable<IInternalNode>)i).Select(n => n.Id).AsEnumerable()))
//                .Register((a, i) => a.Arguments[0] == null, (c, a, i, s) => { throw new ScriptProcessingException("No empty argument is allowed for rename function processing"); })
//                .Register((a, i) => true, (c, a, i, s) => { throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing"); });
//        }

//        public async Task<object> Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, object input, ExecutionScope scope)
//        {
//            throw new NotSupportedException();
//        }


//        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            if (argumentSet.Arguments.Length == 1)
//            {
//                ProcessByInput(context, parameterSet, argumentSet, input, scope, output);
//            }
//            else if (argumentSet.Arguments.Length == 2)
//            {
//                ProcessByArgument(context, parameterSet, argumentSet, input, scope, output);
//            }
//            else
//            {
//                throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing");
//            }
//        }

//        private void ProcessByArgument(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            var task = Task.Run(async () =>
//            {
//                var converter = _argumentConverterSelector.Select(argumentSet);
//                var idsToRename = await converter(context, argumentSet, scope);
//                if (idsToRename.Any())
//                {
//                    var newName = (string)(argumentSet.Arguments.Length == 2 ? argumentSet.Arguments[1] : argumentSet.Arguments[0]);

//                    foreach (var idToRename in idsToRename)
//                    {
//                        var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(idToRename, newName, scope);
//                        output.OnNext(renamedItem);
//                    }
//                }

//                output.OnCompleted();
//            });
//            task.Wait();
//        }

//        private void ProcessByInput(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
//        {
//            input.Subscribe(
//                onError: output.OnError,
//                onCompleted: output.OnCompleted,
//                onNext: o =>
//                {
//                    var task = Task.Run(async () =>
//                    {
//                        var converter = _inputConverterSelector.Select(argumentSet, o);
//                        var idsToRename = await converter(context, argumentSet, o, scope);
//                        if (idsToRename.Any())
//                        {
//                            var newName =
//                                (string)
//                                    (argumentSet.Arguments.Length == 2
//                                        ? argumentSet.Arguments[1]
//                                        : argumentSet.Arguments[0]);

//                            foreach (var idToRename in idsToRename)
//                            {
//                                var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(idToRename, newName, scope);
//                                output.OnNext(renamedItem);
//                            }
//                        }
//                    });
//                    task.Wait();
//                }
//            );
//        }


//        private async Task<IEnumerable<Identifier>> ConvertPathToIds(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
//        {
//            var outputObservable = Observable.Create<object>(async outputObserver =>
//            {
//                await context.PathProcessor.Process(pathSubject, scope, outputObserver);

//                return Disposable.Empty;
//            });

//            var result = outputObservable.ToEnumerable()
//                .Cast<IReadOnlyEntry>()
//                .Select(e => e.Id)
//                .ToArray();

//            return await Task.FromResult(result);
//        }
//    }
//}