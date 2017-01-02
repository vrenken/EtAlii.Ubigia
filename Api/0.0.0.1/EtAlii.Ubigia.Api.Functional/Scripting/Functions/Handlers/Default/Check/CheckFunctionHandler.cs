namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class CheckFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets
        {
            get { return _parameterSets; }
        }

        private readonly ParameterSet[] _parameterSets;

        public string Name
        {
            get { return _name; }
        }

        private readonly string _name;

        public CheckFunctionHandler()
        {
            _parameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof (object))),
            };
            _name = "Check";
        }

        public void Process(
            IFunctionContext context, 
            ParameterSet parameterSet, 
            ArgumentSet argumentSet, 
            IObservable<object> input, 
            ExecutionScope scope, 
            IObserver<object> output, 
            bool processAsSubject)
        {
            output.OnCompleted();
        }
    }
}