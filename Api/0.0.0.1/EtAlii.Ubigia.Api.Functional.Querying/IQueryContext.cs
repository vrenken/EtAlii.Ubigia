namespace EtAlii.Ubigia.Api.Functional.Querying
{
    using System;

    public interface IQueryContext
    {
        IObservable<QueryResult> Execute(string text);
        IObservable<QueryResult> Execute(string text, IQueryScope scope);
        //IObservable<QueryResult> Execute(string text, params object[] args);
    }
}