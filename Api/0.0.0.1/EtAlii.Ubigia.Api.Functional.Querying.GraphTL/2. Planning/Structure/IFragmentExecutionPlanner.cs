namespace EtAlii.Ubigia.Api.Functional 
{
    using EtAlii.Ubigia.Api.Functional.Scripting;

    internal interface IFragmentExecutionPlanner : IExecutionPlanner
    {
        FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata);
    }
}