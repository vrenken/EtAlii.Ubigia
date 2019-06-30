namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Reactive.Linq;
    using System.Threading.Tasks;

    public static class QueryExecutionPlan
    {
        internal static readonly IQueryExecutionPlan Empty = new EmptyExecutionPlan();

        private class EmptyExecutionPlan : IQueryExecutionPlan
        {
            public Type OutputType { get; }

            public EmptyExecutionPlan()
            {
                OutputType = GetType();
            }

            public Task<IObservable<object>> Execute(QueryExecutionScope scope)
            {
                return Task.FromResult(Observable.Empty<object>());
            }

            public override string ToString()
            {
                return "[Empty]";
            }
        }
    }
}