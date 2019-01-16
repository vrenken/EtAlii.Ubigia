namespace EtAlii.Ubigia.Api.Functional.NET47
{
    using System;
    using EtAlii.Ubigia.Api.Functional;

    public class FileFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public FileFunctionHandler()
        {
            ParameterSets = new ParameterSet[] {};
            Name = "File";
        }

        public void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
