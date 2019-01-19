namespace EtAlii.Ubigia.Api.Functional.NET47
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class FormatFunctionHandler : IFunctionHandler
    {
        public ParameterSet[] ParameterSets { get; }

        public string Name { get; }

        public FormatFunctionHandler()
        {
            ParameterSets = new ParameterSet[] {};
            Name = "Format";
        }

        public Task Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}
