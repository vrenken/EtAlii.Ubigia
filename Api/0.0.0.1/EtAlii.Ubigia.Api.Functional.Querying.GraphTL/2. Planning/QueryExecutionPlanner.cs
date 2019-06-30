namespace EtAlii.Ubigia.Api.Functional
{
    internal class QueryExecutionPlanner : IQueryExecutionPlanner
    {
        private readonly IFragmentExecutionPlannerSelector _fragmentExecutionPlannerSelector;

        public QueryExecutionPlanner(IFragmentExecutionPlannerSelector fragmentExecutionPlannerSelector)
        {
            _fragmentExecutionPlannerSelector = fragmentExecutionPlannerSelector;
        }

        public FragmentExecutionPlan Plan(Query query)
        {
            var fragment = query.Structure;
            var fragmentExecutionPlanner = _fragmentExecutionPlannerSelector.Select(fragment);
            return fragmentExecutionPlanner.Plan(fragment);
        }
    }
}