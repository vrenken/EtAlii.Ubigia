namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class RootDefinitionSubjectProcessor : IRootDefinitionSubjectProcessor
    {
        //private readonly IPathSubjectForOutputConverter _converter;

        public RootDefinitionSubjectProcessor()
        {
        }
         
        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootDefinitionSubject = (RootDefinitionSubject) subject;
            output.OnNext(rootDefinitionSubject);
            output.OnCompleted();
        }
    }
}
