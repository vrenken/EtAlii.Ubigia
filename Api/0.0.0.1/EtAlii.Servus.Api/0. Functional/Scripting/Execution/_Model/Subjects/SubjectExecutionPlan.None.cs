namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Reactive.Linq;

    public static class SubjectExecutionPlan
    {
        public static readonly ISubjectExecutionPlan Empty = new EmptySubjectExecutionPlan();

        private class EmptySubjectExecutionPlan : ISubjectExecutionPlan
        {
            public Type OutputType { get; private set; }
            public Subject Subject { get; private set; }

            public EmptySubjectExecutionPlan()
            {
                OutputType = GetType();
                Subject = new EmptySubject();
            }

            public IObservable<object> Execute(ExecutionScope scope)
            {
                return Observable.Empty<object>();
            }

            public override string ToString()
            {
                return "[Empty]";
            }
        }
    }
}