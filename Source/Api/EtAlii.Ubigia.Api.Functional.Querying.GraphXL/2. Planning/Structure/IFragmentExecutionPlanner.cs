namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    internal interface IFragmentExecutionPlanner : IExecutionPlanner
    {
        FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata);
    }
}
