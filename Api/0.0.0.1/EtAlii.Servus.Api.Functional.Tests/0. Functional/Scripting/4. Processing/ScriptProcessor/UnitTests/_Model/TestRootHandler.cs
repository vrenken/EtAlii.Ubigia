namespace EtAlii.Servus.Api.Functional.Tests
{
    using System;

    public class TestRootHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get; }

        public TestRootHandler(PathSubjectPart[] template)
        {
            Template = template;
        }

        public void Process(IRootContext context, IObservable<object> input, ExecutionScope scope, IObserver<object> output, bool processAsSubject)
        {
            throw new NotImplementedException();
        }
    }
}