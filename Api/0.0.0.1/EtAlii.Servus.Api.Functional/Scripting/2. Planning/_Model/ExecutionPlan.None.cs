namespace EtAlii.Servus.Api.Functional
{
    using System;
    using System.Reactive.Linq;

    public static class ExecutionPlan
    {
        public static readonly IExecutionPlan Empty = new EmptyExecutionPlan();

        private class EmptyExecutionPlan : IExecutionPlan
        {
            public Type OutputType { get; private set; }

            public EmptyExecutionPlan()
            {
                OutputType = GetType();
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