﻿namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;

    public class TestFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets => _parameterSets;
        private readonly ParameterSet[] _parameterSets;

        public string Name => _name;
        private readonly string _name;

        public TestFunctionHandler()
        {
            _parameterSets = new []
            {
                new ParameterSet(false),
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof(object))),
                new ParameterSet(true, new Parameter("var", typeof(string))),
                new ParameterSet(false, new Parameter("var1", typeof(object)), new Parameter("var2", typeof(object))),
                new ParameterSet(false, new Parameter("var1", typeof(string)), new Parameter("var2", typeof(string)), new Parameter("var3", typeof(string))),
                new ParameterSet(true, new Parameter("var1", typeof(string)), new Parameter("var2", typeof(string)), new Parameter("var3", typeof(string))),
            };
            _name = "Function";
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}