namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryExecutionPlanningScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IQueryExecutionPlanner, QueryExecutionPlanner>();
            
            container.Register<IMutationFragmentExecutionPlanner, MutationFragmentExecutionPlanner>();
            container.Register<IQueryFragmentExecutionPlanner, QueryFragmentExecutionPlanner>();
            container.Register<IFragmentExecutionPlannerSelector, FragmentExecutionPlannerSelector>();
            
        }
    }
}
