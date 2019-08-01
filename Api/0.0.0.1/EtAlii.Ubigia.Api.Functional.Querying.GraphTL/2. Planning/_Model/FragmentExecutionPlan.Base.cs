namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    public abstract class FragmentExecutionPlan
    {
        public abstract Type OutputType { get; }

        internal abstract Task<IObservable<Structure>> Execute(QueryExecutionScope executionScope);
    }
}