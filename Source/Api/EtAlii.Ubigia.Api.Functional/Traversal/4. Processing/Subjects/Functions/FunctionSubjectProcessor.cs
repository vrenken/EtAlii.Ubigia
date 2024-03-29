﻿// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;

internal class FunctionSubjectProcessor : IFunctionSubjectProcessor
{
    private readonly IFunctionContext _functionContext;
    private readonly IParameterSetFinder _parameterSetFinder;
    private readonly IFunctionHandlerFinder _functionHandlerFinder;
    private readonly IArgumentSetFinder _argumentSetFinder;


    public FunctionSubjectProcessor(
        IFunctionContext functionContext,
        IParameterSetFinder parameterSetFinder,
        IFunctionHandlerFinder functionHandlerFinder,
        IArgumentSetFinder argumentSetFinder)
    {
        _functionContext = functionContext;
        _parameterSetFinder = parameterSetFinder;
        _functionHandlerFinder = functionHandlerFinder;
        _argumentSetFinder = argumentSetFinder;
    }

    public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
    {
        var functionSubject = (FunctionSubject)subject;

        // Find matching argument set.
        var argumentSet = _argumentSetFinder.Find(functionSubject, scope);
        // Find function handler.
        var functionHandler = _functionHandlerFinder.Find(functionSubject);
        // And one single parameter set with the exact same parameters.
        var parameterSet = _parameterSetFinder.Find(functionSubject, functionHandler, argumentSet);

        await functionHandler
            .Process(_functionContext, parameterSet, argumentSet, Observable.Empty<object>(), scope, output, true)
            .ConfigureAwait(false);
    }
}
