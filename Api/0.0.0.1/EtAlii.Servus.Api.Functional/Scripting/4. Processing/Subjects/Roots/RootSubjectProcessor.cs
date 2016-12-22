namespace EtAlii.Servus.Api.Functional
{
    using System;

    internal class RootSubjectProcessor : IRootSubjectProcessor
    {
        //private readonly IPathSubjectForOutputConverter _converter;

        public RootSubjectProcessor()
        {
        }
         
        public void Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var rootSubject = (RootSubject)subject;
            output.OnNext(rootSubject);
            output.OnCompleted();
        }
    }
}
