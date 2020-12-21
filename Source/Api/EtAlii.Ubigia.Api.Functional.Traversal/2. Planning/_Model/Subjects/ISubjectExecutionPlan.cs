namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface ISubjectExecutionPlan : IScriptExecutionPlan
    {
        Subject Subject { get; }
    }
}
