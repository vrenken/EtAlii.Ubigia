namespace EtAlii.Ubigia.Api.Functional 
{
    using System.Collections.ObjectModel;

    internal interface IQueryExecutionPlanner
    {
        FragmentExecutionPlan[] Plan(Query query, out ObservableCollection<Structure> structure);
    }
}
