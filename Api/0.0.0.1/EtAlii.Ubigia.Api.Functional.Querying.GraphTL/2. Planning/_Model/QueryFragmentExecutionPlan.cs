namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class QueryFragmentExecutionPlan : FragmentExecutionPlanBase<QueryFragment>
    {
        private readonly IQueryFragmentProcessor _processor;
        //public ObservableCollection<Structure> ParentChildren { get; } = new ObservableCollection<Structure>();
        //public ObservableCollection<Value> ParentValues { get; } = new ObservableCollection<Value>();

        public QueryFragmentExecutionPlan(
            QueryFragment fragment,
            FragmentContext fragmentContext,
            IQueryFragmentProcessor processor)
            : base(fragment, fragmentContext)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(QueryFragment);
        }

        protected override Task Execute(FragmentContext fragmentContext, QueryExecutionScope executionScope, IObserver<Structure> output)
        {
            return _processor.Process(Fragment, executionScope, fragmentContext, output);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}