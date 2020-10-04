namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface ISubjectExecutionPlan : IScriptExecutionPlan
    {
        Subject Subject { get; }
    }
}