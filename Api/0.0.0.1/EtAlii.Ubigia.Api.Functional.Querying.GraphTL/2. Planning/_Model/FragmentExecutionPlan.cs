namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal abstract class FragmentExecutionPlan
    {
        public abstract Type OutputType { get; }

        public abstract Task<IObservable<Structure>> Execute(QueryExecutionScope executionScope);
    }
}