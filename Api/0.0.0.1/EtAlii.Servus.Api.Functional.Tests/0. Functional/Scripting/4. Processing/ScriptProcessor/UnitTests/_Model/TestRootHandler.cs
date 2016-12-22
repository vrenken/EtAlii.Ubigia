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

        public void Process(IRootContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            throw new NotImplementedException();
        }
    }
}