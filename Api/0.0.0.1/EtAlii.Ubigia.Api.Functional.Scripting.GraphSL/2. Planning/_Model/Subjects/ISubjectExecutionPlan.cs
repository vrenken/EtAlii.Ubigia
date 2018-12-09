namespace EtAlii.Ubigia.Api.Functional
{
    public interface ISubjectExecutionPlan : IExecutionPlan
    {
        Subject Subject { get; }
    }
}