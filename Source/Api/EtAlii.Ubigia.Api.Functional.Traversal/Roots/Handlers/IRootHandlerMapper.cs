namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IRootHandlerMapper
    {
        string Name { get; }

        IRootHandler[] AllowedRootHandlers { get; }

        //string TypeName [ get; ]

        //ParameterSet[] ParameterSets [ get; ]
        //void Process(IRootContext context, ParameterSet parameterSet, ArgumentSet argumentSet, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        //void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
    }
}
