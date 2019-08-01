namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.xTechnology.MicroContainer;

    internal class QueryExecutionPlanningScaffolding : IScaffolding
    {
        public void Register(Container container)
        {
            container.Register<IQueryExecutionPlanner, QueryExecutionPlanner>();
        }
    }
}
