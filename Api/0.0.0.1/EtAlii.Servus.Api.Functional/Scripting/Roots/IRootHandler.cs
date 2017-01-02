namespace EtAlii.Servus.Api.Functional
{
    using System;

    public interface IRootHandler
    {
        string Name { get; }

        IRootSubHandler[] AllowedPaths { get; }

        //string TypeName { get; }

        //ParameterSet[] ParameterSets { get; }
        //void Process(IRootContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
        void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
    }
}