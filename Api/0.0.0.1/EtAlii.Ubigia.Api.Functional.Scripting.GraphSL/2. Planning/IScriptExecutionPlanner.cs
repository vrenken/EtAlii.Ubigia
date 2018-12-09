namespace EtAlii.Ubigia.Api.Functional
{
    internal interface IScriptExecutionPlanner
    {
        ISequenceExecutionPlan[] Plan(Script script);
    }
}
