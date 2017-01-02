namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class TestRenameFunctionHandler : IFunctionHandler
    {
        public string Name { get { return _name; } }
        private readonly string _name;

        public ParameterSet[] ParameterSets { get { return _parameterSets; } }
        private readonly ParameterSet[] _parameterSets;

        public TestRenameFunctionHandler()
        {
            _name = "TestRename";
            _parameterSets = new[]
            {
                new ParameterSet(false, new Parameter("source", typeof(string))),
                new ParameterSet(false, new Parameter("source", typeof(string)), new Parameter("destination", typeof(string))),
            };
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
