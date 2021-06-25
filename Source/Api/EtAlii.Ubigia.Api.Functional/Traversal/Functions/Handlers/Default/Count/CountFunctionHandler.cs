// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reactive.Disposables;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Logical;

    internal class CountFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public CountFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
                new ParameterSet(false, new Parameter("var", typeof(Identifier))),
                new ParameterSet(false, new Parameter("var", typeof(IInternalNode))),
                new ParameterSet(false, new Parameter("var", typeof(IObservable<object>))),
            };
            Name = "Count";
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            if (processAsSubject)
            {
                if (argumentSet.Arguments.Length == 0)
                {
                    output.OnCompleted();
                }
                else
                {
                    input = argumentSet.Arguments[0] as IObservable<object>;
                    if (input == null)
                    {
                        throw new ScriptProcessingException("Unable to convert arguments for Count function processing");
                    }
                }
            }
            else
            {
                if (argumentSet.Arguments.Length == 1)
                {
                    throw new ScriptProcessingException("Unable to use arguments and input data for Count function processing");
                }
            }
            Process(context, input, scope, output);
            return Task.CompletedTask;
        }

        private void Process(IFunctionContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output)
        {
            var result = 0;

            input.SubscribeAsync(
                onError: output.OnError,
                onCompleted: () =>
                {
                    output.OnNext(result);
                    output.OnCompleted();
                },
                onNext: async o =>
                {
                    result += o switch
                    {
                        PathSubject pathSubject => await CountPath(context, pathSubject, scope).ConfigureAwait(false),
                        Identifier => 1,
                        IInternalNode => 1,
                        IObservable<object> observable => await CountObservable(observable).ConfigureAwait(false),
                        IEnumerable<Identifier> identifiers => identifiers.Count(),
                        IEnumerable<IInternalNode> nodes => nodes.Count(),
                        null => throw new ScriptProcessingException("No empty argument is allowed for Count function processing"),
                        _ => throw new ScriptProcessingException("Unable to convert arguments for Count function processing")
                    };
                });
        }

        private async Task<int> CountPath(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
        {
            var outputObservable = Observable.Create<object>(async outputObserver =>
            {
                await context.PathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

                return Disposable.Empty;
            });

            var result = await outputObservable.Count();

            return result;
        }

        private async Task<int> CountObservable(IObservable<object> observable)
        {
            var result = await observable.Count();
            return result;
        }
    }
}
