namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;

    internal class QueryFragmentExecutionPlan : FragmentExecutionPlanBase
    {
        private readonly IQueryFragmentProcessor _processor;

        public QueryFragmentExecutionPlan(
            Fragment fragment,
            IQueryFragmentProcessor processor)
            :base(fragment)
        {
            _processor = processor;
        }

        protected override Type GetOutputType()
        {
            return typeof(QueryFragment);
        }

        protected override Task Execute(QueryExecutionScope scope, IObserver<object> output)
        {
            return _processor.Process(Fragment, scope, output);
        }

        public override string ToString()
        {
            return Fragment.ToString();
        }
    }
}