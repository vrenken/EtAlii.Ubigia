// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using EtAlii.Ubigia.Api.Logical;

internal class RenameFunctionHandler : IFunctionHandler
{
    public ParameterSet[] ParameterSets { get; }

    public string Name { get; }

    public RenameFunctionHandler()
    {
        ParameterSets = new[]
        {
            new ParameterSet(true, new Parameter("name", typeof(string))),
            new ParameterSet(true, new Parameter("name", typeof(IObservable<object>))),
            new ParameterSet(false, new Parameter("var", typeof(string)), new Parameter("name", typeof(string))),
            new ParameterSet(false, new Parameter("var", typeof(PathSubject)), new Parameter("name", typeof(string))),
            new ParameterSet(false, new Parameter("var", typeof(IObservable<object>)), new Parameter("name", typeof(string))),
            new ParameterSet(false, new Parameter("var", typeof(string)), new Parameter("name", typeof(IObservable<object>))),
            new ParameterSet(false, new Parameter("var", typeof(PathSubject)), new Parameter("name", typeof(IObservable<object>))),
            new ParameterSet(false, new Parameter("var", typeof(IObservable<object>)), new Parameter("name", typeof(IObservable<object>)))
        };
        Name = "Rename";
    }

    public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    {
        if (processAsSubject)
        {
            if (argumentSet.Arguments.Length == 2)
            {
                ProcessByArgument(context, argumentSet, scope, output); // parameterSet,
            }
            else
            {
                // No way to throw an exception here. It could be a left side subject so we will have to wait until it is executed from an operator.
                //throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing")
                output.OnCompleted();
            }
        }
        else
        {
            if (argumentSet.Arguments.Length == 1)
            {
                ProcessByInput(context, argumentSet, input, scope, output); // parameterSet,
            }
            else
            {
                throw new ScriptProcessingException("Unable to convert arguments and input for rename function processing");
            }
        }
        return Task.CompletedTask;
    }

    private void ProcessByArgument(
        IFunctionContext context,
        ArgumentSet argumentSet,
        ExecutionScope scope,
        IObserver<object> output)
    {
        var c = argumentSet.Arguments.Length;
        var f = argumentSet.Arguments.Length > 0 ? argumentSet.Arguments[0] : null;
        var s = argumentSet.Arguments.Length > 1 ? argumentSet.Arguments[1] : null;

        var newName = (c, f, s) switch
        {
            (1, string st, _) => st,
            (1, IObservable<object> observable, _) => observable.ToEnumerable().Cast<string>().Single(),
            (2, _, string st) => st,
            (2, _, IObservable<object> observable) => observable.ToEnumerable().Cast<string>().Single(),
            (_,_,_) => throw new ScriptProcessingException("Unable to convert name input for Rename function processing")
        };

        if (argumentSet.Arguments[0] is not IObservable<object> input)
        {
            throw new ScriptProcessingException("Unable to convert arguments for Rename function processing");
        }
        input.SubscribeAsync(
            onError: output.OnError,
            onCompleted: output.OnCompleted,
            onNext: async o =>
            {
                var results = ToIdentifiers(context, o, scope);
                foreach (var result in results)
                {

                    var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(result, newName, scope).ConfigureAwait(false);
                    output.OnNext(renamedItem);
                }
            });
    }

    private IEnumerable<Identifier> ToIdentifiers(IFunctionContext context, object o, ExecutionScope scope)
    {
        return o switch
        {
            PathSubject pathSubject => ConvertPathToIds(context, pathSubject, scope),
            Identifier identifier => new [] { identifier },
            Node internalNode => new [] { internalNode.Id },
            _ => throw new ScriptProcessingException("Unable to convert input for Rename function processing")
        };
    }
    private void ProcessByInput(
        IFunctionContext context,
        ArgumentSet argumentSet,
        IObservable<object> input,
        ExecutionScope scope,
        IObserver<object> output)
    {
        var newName = (string)(argumentSet.Arguments.Length == 2 ? argumentSet.Arguments[1] : argumentSet.Arguments[0]);

        input.SubscribeAsync(
            onError: output.OnError,
            onCompleted: output.OnCompleted,
            onNext: async o =>
            {
                var results = ToIdentifiers(context, o, scope);
                foreach (var result in results)
                {
                    var renamedItem = await context.PathProcessor.Context.Logical.Nodes.Rename(result, newName, scope).ConfigureAwait(false);
                    output.OnNext(renamedItem);
                }
            });
    }

    private IEnumerable<Identifier> ConvertPathToIds(IFunctionContext context, PathSubject pathSubject, ExecutionScope scope)
    {
        var outputObservable = Observable.Create<object>(async outputObserver =>
        {
            await context.PathProcessor.Process(pathSubject, scope, outputObserver).ConfigureAwait(false);

            return Disposable.Empty;
        });

        return outputObservable
            .Select(context.ItemToIdentifierConverter.Convert)
            .ToEnumerable();
    }
}
