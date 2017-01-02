namespace EtAlii.Ubigia.Api.Functional
{
    using System;

    public interface IRootHandler
    {

        PathSubjectPart[] Template { get; }
        void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output);
    }
}