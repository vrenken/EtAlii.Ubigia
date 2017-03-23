namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    internal class CheckFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public CheckFunctionHandler()
        {
            ParameterSets = new[]
            {
                new ParameterSet(true),
                new ParameterSet(false, new Parameter("var", typeof (object))),
            };
            Name = "Check";
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