namespace EtAlii.Ubigia.Api.Functional.Tests
{
    using System;

    public class TestFormatFunctionHandler : IFunctionHandler
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public TestFormatFunctionHandler()
        {
            _name = "TestFormat";
            _parameterSets = new[]
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
