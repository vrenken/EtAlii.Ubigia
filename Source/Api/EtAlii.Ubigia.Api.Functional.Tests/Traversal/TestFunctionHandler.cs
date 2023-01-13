// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests;

using System;
using System.Threading.Tasks;

public class TestFunctionHandler : IFunctionHandler
{
    public ParameterSet[] ParameterSets { get; }

    public string Name { get; }

    public TestFunctionHandler()
    {
        ParameterSets = new []
        {
            new ParameterSet(false),
            new ParameterSet(true),
            new ParameterSet(false, new Parameter("var", typeof(object))),
            new ParameterSet(true, new Parameter("var", typeof(string))),
            new ParameterSet(false, new Parameter("var1", typeof(object)), new Parameter("var2", typeof(object))),
            new ParameterSet(false, new Parameter("var1", typeof(string)), new Parameter("var2", typeof(string)), new Parameter("var3", typeof(string))),
            new ParameterSet(true, new Parameter("var1", typeof(string)), new Parameter("var2", typeof(string)), new Parameter("var3", typeof(string))),
        };
        Name = "Function";
    }

    public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    {
        throw new NotImplementedException();
    }
}
