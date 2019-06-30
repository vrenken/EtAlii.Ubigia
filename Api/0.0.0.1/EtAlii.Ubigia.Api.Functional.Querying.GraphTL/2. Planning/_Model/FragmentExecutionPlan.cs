namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class FragmentExecutionPlan : IQueryExecutionPlan
    {
        private readonly Fragment _fragment;

        public Type OutputType { get; }

        public FragmentExecutionPlan(Fragment fragment)
        {
            _fragment = fragment;
            OutputType = GetType();
        }

        public Task<IObservable<object>> Execute(QueryExecutionScope scope)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "[FRAGMENT]" + _fragment + "[/FRAGMENT]";
        }
    }
}