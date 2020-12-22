namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IFragmentExecutionPlanner : IExecutionPlanner
    {
        FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata);
    }
}
