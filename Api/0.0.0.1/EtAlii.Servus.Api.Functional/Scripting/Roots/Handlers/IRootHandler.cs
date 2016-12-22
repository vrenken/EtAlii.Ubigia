namespace EtAlii.Servus.Api.Functional
{
    using System;

    public interface IRootHandler
    {

        PathSubjectPart[] Template { get; }
        void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject);
    }
}