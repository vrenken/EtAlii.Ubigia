﻿namespace EtAlii.Ubigia.Api.Functional 
{
    internal interface IFragmentExecutionPlanner : IExecutionPlanner
    {
        FragmentExecutionPlan Plan(Fragment fragment, FragmentMetadata fragmentMetadata);
    }
}