namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IQueryExecutionPlanner
    {
        void Plan(
            Query query, 
            out FragmentMetadata rootFragmentMetadata, 
            out FragmentExecutionPlan[] fragmentExecutionPlans);
    }
}
