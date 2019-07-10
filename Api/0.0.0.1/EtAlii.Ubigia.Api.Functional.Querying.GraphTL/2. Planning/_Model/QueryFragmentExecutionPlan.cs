namespace EtAlii.Ubigia.Api.Functional 
{
    using System;
    using System.Threading.Tasks;

    internal class QueryFragmentExecutionPlan : FragmentExecutionPlanBase<QueryFragment>
    {
        private readonly IQueryFragmentProcessor _processor;

        public QueryFragmentExecutionPlan(
            QueryFragment fragment,
            FragmentMetadata fragmentMetadata,
            IQueryFragmentProcessor processor)
            : base(fragment, fragmentMetadata)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(QueryFragment);
        }

        protected override Task Execute(FragmentMetadata fragmentMetadata, QueryExecutionScope executionScope, IObserver<Structure> output)
        {
            return _processor.Process(Fragment, executionScope, fragmentMetadata, output);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}