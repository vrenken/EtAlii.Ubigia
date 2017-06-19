﻿namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;

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

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }

    }
}
