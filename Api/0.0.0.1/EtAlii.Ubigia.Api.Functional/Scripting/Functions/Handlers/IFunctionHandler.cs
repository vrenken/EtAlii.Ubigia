namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface IFunctionHandler
    {
        ParameterSet[] ParameterSets { get; }
        string Name { get; }

        void Process(IFunctionContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
    }
}