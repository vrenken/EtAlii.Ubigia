namespace EtAlii.Ubigia.Api.Functional 
{
    internal class ValueQueryExecutionPlanner : IValueQueryExecutionPlanner
    {
        private readonly IValueQueryProcessor _queryProcessor;

        public ValueQueryExecutionPlanner(IValueQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public FragmentExecutionPlan Plan(Fragment fragment, FragmentContext fragmentContext)
        {
            var valueQuery = (ValueQuery)fragment;
            return new QueryFragmentExecutionPlan(valueQuery, fragmentContext, _queryProcessor);
        }
    }
}