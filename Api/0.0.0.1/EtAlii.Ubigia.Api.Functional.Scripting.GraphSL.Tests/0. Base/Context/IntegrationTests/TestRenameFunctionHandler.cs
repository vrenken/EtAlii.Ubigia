﻿namespace EtAlii.Ubigia.Api.Functional.Scripting.GraphSL.Tests
{
    using System;
    using System.Threading.Tasks;

    public class TestRenameFunctionHandler : IFunctionHandler
    {
        public string Name { get; }

        public ParameterSet[] ParameterSets { get; }

        public TestRenameFunctionHandler()
        {
            Name = "TestRename";
            ParameterSets = new[]
            {
                new ParameterSet(false, new Parameter("source", typeof(string))),
                new ParameterSet(false, new Parameter("source", typeof(string)), new Parameter("destination", typeof(string))),
            };
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
