// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;
    using System.Threading.Tasks;

    public class TestFormatFunctionHandler : IFunctionHandler
    {
        public string Name { get; }

        public ParameterSet[] ParameterSets { get; }

        public TestFormatFunctionHandler()
        {
            Name = "TestFormat";
            ParameterSets = new[]
            {
                new ParameterSet(false, new Parameter("value1", typeof(string))),
                new ParameterSet(false, new Parameter("value1", typeof(string)), new Parameter("value2", typeof(string))),
            };
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }

    }
}
