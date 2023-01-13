// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using EtAlii.Ubigia.Api.Logical;

public class FunctionHandlerBase
{
    protected IEnumerable<Identifier> ToIdentifierObservable(IFunctionContext context, object o, ExecutionScope scope)
    {
        return o switch
        {
            PathSubject pathSubject => ConvertPathToIds(context, pathSubject, scope),
            Identifier identifier => new [] { identifier },
            Node node => new [] { node.Id },
            _ => throw new ScriptProcessingException("Unable to convert input for Function processing")
        };
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
