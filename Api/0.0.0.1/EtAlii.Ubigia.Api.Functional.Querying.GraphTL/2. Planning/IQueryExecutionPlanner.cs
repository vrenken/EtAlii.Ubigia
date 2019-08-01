namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IQueryExecutionPlanner
    {
        FragmentExecutionPlan[] Plan(Query query);
    }
}
