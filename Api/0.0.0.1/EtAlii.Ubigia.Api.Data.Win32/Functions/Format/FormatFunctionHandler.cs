namespace EtAlii.Ubigia.Api.Functional.Win32
{
    using System;
    using EtAlii.Ubigia.Api.Functional;

    public class FormatFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets => _parameterSets;
        private readonly ParameterSet[] _parameterSets;

        public string Name => _name;
        private readonly string _name;

        public FormatFunctionHandler()
        {
            _parameterSets = new ParameterSet[] {};
            _name = "Format";
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
