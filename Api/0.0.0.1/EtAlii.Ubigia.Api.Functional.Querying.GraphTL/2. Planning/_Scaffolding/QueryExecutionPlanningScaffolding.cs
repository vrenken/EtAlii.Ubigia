namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryExecutionPlanningScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IQueryExecutionPlanner, QueryExecutionPlanner>();
            
            container.Register<IValueMutationExecutionPlanner, ValueMutationExecutionPlanner>();
            container.Register<IStructureMutationExecutionPlanner, StructureMutationExecutionPlanner>();
            container.Register<IValueQueryExecutionPlanner, ValueQueryExecutionPlanner>();
            container.Register<IStructureQueryExecutionPlanner, StructureQueryExecutionPlanner>();
            container.Register<IFragmentExecutionPlannerSelector, FragmentExecutionPlannerSelector>();
            
        }
    }
}
