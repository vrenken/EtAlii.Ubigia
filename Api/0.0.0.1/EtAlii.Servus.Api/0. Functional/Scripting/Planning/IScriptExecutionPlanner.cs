namespace EtAlii.Servus.Api.Functional
{
    internal interface IScriptExecutionPlanner
    {
        ISequenceExecutionPlan[] Plan(Script script);
    }
}
