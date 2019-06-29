namespace EtAlii.Ubigia.Api.Functional
{
    public interface ISubjectExecutionPlan : IScriptExecutionPlan
    {
        Subject Subject { get; }
    }
}