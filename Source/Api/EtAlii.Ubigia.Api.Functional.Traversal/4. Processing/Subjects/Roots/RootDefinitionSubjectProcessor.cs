namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    using System.Threading.Tasks;

    internal class RootDefinitionSubjectProcessor : IRootDefinitionSubjectProcessor
    {
        //private readonly IPathSubjectForOutputConverter _converter

        public Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootDefinitionSubject = (RootDefinitionSubject) subject;
            output.OnNext(rootDefinitionSubject);
            output.OnCompleted();

            return Task.CompletedTask;
        }
    }
}
