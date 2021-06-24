// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class IncludeFunctionHandler : FunctionHandlerBase, IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public IncludeFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true, new Parameter("var", typeof(PathSubject))),
                new ParameterSet(true, new Parameter("var", typeof(IObservable<object>))),
            };
            Name = "Include";
        }

        public async Task Process(
            IFunctionContext context,
            ParameterSet parameterSet,
            ArgumentSet argumentSet,
            IObservable<object> input,
            ExecutionScope scope,
            IObserver<object> output,
            bool processAsSubject)
        {
            if (processAsSubject)
            {
                //if [argumentSet.Arguments.Length = = 1]
                //[
                //    ProcessByArgument(context, parameterSet, argumentSet, scope, output)
                //]
                //else
                //[
                    // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                    //throw new ScriptProcessingException("Unable to convert arguments for rename function processing")
                    output.OnCompleted();
                //]
            }
            else
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    await ProcessByInput(context, argumentSet, input, scope, output).ConfigureAwait(false);
                }
                else
                {
                    throw new ScriptProcessingException("Unable to convert arguments and input for Include function processing");
                }
            }
        }

        //private void ProcessByArgument(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, ExecutionScope scope, IObserver<object> output)
        //[
        //    var input = argumentSet.Arguments[0] as IObservable<object>
        //    if [input = = null]
        //    [
        //        throw new ScriptProcessingException("Unable to convert arguments for Include function processing")
        //    ]
        //    input.Subscribe(
        //        onError: (e) => output.OnError(e),
        //        onCompleted: () => output.OnCompleted(),
        //        onNext: o =>
        //        [
        //            var converter = ToIdentifierConverterSelector.Select(o)
        //            var results = converter(context, o, scope)
        //            foreach (var result in results.ToEnumerable())
        //            [
        //                output.OnNext(result)
        //            ]
        //        ])
        //]
        private async Task ProcessByInput(
            IFunctionContext context,
            ArgumentSet argumentSet,
            IObservable<object> input,
            ExecutionScope scope,
            IObserver<object> output)
        {
            if (!(argumentSet.Arguments[0] is PathSubject pathSubject))
            {
                if (!(argumentSet.Arguments[0] is IObservable<object> argumentInput))
                {
                    throw new ScriptProcessingException("Unable to convert arguments for Include function processing");
                }

                pathSubject = await argumentInput.Cast<PathSubject>();
            }
            if (pathSubject == null)
            {
                throw new ScriptProcessingException("Unable to convert arguments for Include function processing");
            }


            input.Subscribe(
                onError: output.OnError,
                onCompleted: output.OnCompleted,
                onNext: o =>
                {
                    var results = ToIdentifierObservable(context, o, scope);
                    foreach (var result in results)
                    {
                        // Existing output.
                        var parts = new List<PathSubjectPart>(new [] {new IdentifierPathSubjectPart(result) });
                        var path = new AbsolutePathSubject(parts.ToArray());
                        Output(context, path, scope, output);

                        // New output
                        parts.AddRange(pathSubject.Parts);
                        path = new AbsolutePathSubject(parts.ToArray());
                        Output(context, path, scope, output);
                    }
                });
        }

        private void Output(IFunctionContext context, AbsolutePathSubject path, ExecutionScope scope, IObserver<object> output)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(path, scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            }).ToHotObservable();
            foreach (var o2 in outputObservable.ToEnumerable())
            {
                output.OnNext(o2);
            }
        }
    }
}
