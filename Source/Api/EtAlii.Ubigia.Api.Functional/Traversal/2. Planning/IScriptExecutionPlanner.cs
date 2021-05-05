namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface IScriptExecutionPlanner
    {
        ISequenceExecutionPlan[] Plan(Script script);
    }
}
