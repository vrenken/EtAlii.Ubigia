namespace EtAlii.Servus.Api.Functional
{
    using System;
    public interface IRootHandlerMapper
    {
        string Name { get; }

        IRootHandler[] AllowedPaths { get; }

        //string TypeName { get; }

        //ParameterSet[] ParameterSets { get; }
        //void Process(IRootContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
        void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
    }
}