// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

public interface IFunctionHandler
{
    ParameterSet[] ParameterSets { get; }
    string Name { get; }

    Task Process(
        IFunctionContext context,
        ParameterSet parameterSet,
        ArgumentSet argumentSet,
        IObservable<object> input,
        ExecutionScope scope,
        IObserver<object> output,
        bool processAsSubject);
}
