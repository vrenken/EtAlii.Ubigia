namespace EtAlii.Ubigia.Api.Functional.Scripting
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