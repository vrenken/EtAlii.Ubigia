﻿namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IExecutionPlanCombiner
    {
        ISubjectExecutionPlan Combine(
            IExecutionPlanner planner, 
            SequencePart currentPart, 
            SequencePart nextPart,
            ISubjectExecutionPlan rightExecutionPlan, 
            out bool skipNext);
    }
}