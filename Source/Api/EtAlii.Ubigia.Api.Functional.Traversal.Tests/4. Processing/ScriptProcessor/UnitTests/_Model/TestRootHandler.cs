namespace EtAlii.Ubigia.Api.Functional.Traversal.Tests
{
    using System;

    internal class TestRootHandler : IRootHandler
    {
        public PathSubjectPart[] Template { get; }

        public TestRootHandler(PathSubjectPart[] template)
        {
            Template = template;
        }

        public void Process(IScriptProcessingContext context, PathSubjectPart[] match, PathSubjectPart[] rest, ExecutionScope scope, IObserver<object> output)
        {
            throw new NotImplementedException();
        }
    }
}
