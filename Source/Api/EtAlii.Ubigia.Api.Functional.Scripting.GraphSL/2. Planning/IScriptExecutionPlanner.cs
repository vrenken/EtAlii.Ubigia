namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface IScriptExecutionPlanner
    {
        ISequenceExecutionPlan[] Plan(Script script);
    }
}
