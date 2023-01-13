// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal;

using System;
using System.Threading.Tasks;

public class FormatFunctionHandler : IFunctionHandler
{
    public ParameterSet[] ParameterSets { get; }

    public string Name { get; }

    public FormatFunctionHandler()
    {
        ParameterSets = Array.Empty<ParameterSet>();
        Name = "Format";
    }

    public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    {
        throw new NotImplementedException();
    }
}
