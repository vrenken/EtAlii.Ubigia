namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal interface IQueryExecutionPlan
    {
        Type OutputType { get; }

        Task<IObservable<object>> Execute(QueryExecutionScope scope);
    }
}