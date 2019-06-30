namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IFragmentExecutionPlan : IQueryExecutionPlan
    {
        Fragment Fragment { get; }
    }
}