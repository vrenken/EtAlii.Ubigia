namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class RootSubjectProcessor : IRootSubjectProcessor
    {
        //private readonly IPathSubjectForOutputConverter _converter

        public Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootSubject = (RootSubject)subject;
            output.OnNext(rootSubject);
            output.OnCompleted();

            return Task.CompletedTask;
        }
    }
}
