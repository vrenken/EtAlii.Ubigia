// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

internal class IdFunctionHandler : FunctionHandlerBase, IFunctionHandler
{
    public ParameterSet[] ParameterSets { get; }

    public string Name { get; }

    public IdFunctionHandler()
    {
        ParameterSets = new[]
        {
            new ParameterSet(true),
            new ParameterSet(false, new Parameter("var", typeof(PathSubject))),
            new ParameterSet(false, new Parameter("var", typeof(IObservable<object>))),
        };
        Name = "Id";
    }

    public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    {
        if (processAsSubject)
        {
            if (argumentSet.Arguments.Length == 1)
            {
                ProcessByArgument(context, argumentSet, scope, output);
            }
            else
            {
                // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                //throw new ScriptProcessingException("Unable to convert arguments for rename function processing")
                output.OnCompleted();
            }
        }
        else
        {
            if (argumentSet.Arguments.Length == 0)
            {
                ProcessByInput(context, input, scope, output); // parameterSet, argumentSet,
            }
            else
            {
                throw new ScriptProcessingException("Unable to convert arguments and input for Id function processing");
            }
        }
        return Task.CompletedTask;
    }

    private void ProcessByArgument(IFunctionContext context, ArgumentSet argumentSet, ExecutionScope scope, IObserver<object> output) // , ParameterSet parameterSet
    {
        if (!(argumentSet.Arguments[0] is IObservable<object> input))
        {
            throw new ScriptProcessingException("Unable to convert arguments for Id function processing");
        }
        input.Subscribe(
            onError: output.OnError,
            onCompleted: output.OnCompleted,
            onNext: o =>
            {
                var results = ToIdentifierObservable(context, o, scope);
                foreach (var result in results)
                {
                    output.OnNext(result);
                }
            });
    }

    private void ProcessByInput(
        IFunctionContext context,
        //ParameterSet parameterSet,
        //ArgumentSet argumentSet,
        IObservable<object> input,
        ExecutionScope scope,
        IObserver<object> output)
    {
        input.Subscribe(
            onError: output.OnError,
            onCompleted: output.OnCompleted,
            onNext: o =>
            {
                var results = ToIdentifierObservable(context, o, scope);
                foreach (var result in results)
                {
                    output.OnNext(result);
                }
            });
    }
}
