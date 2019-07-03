namespace EtAlii.Ubigia.Api.Functional 
{
    internal class StructureQueryExecutionPlanner : IStructureQueryExecutionPlanner
    {
        private readonly IStructureQueryProcessor _queryProcessor;

        public StructureQueryExecutionPlanner(IStructureQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public FragmentExecutionPlan Plan(Fragment fragment, FragmentContext fragmentContext)
        {
            var structureQuery = (StructureQuery)fragment;
            return new QueryFragmentExecutionPlan(structureQuery, fragmentContext, _queryProcessor);
        }
    }
}