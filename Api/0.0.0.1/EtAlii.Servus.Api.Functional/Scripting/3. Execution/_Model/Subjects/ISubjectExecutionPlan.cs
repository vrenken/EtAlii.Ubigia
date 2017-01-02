namespace EtAlii.Servus.Api.Functional
{
    public interface ISubjectExecutionPlan : IExecutionPlan
    {
        Subject Subject { get; }
    }
}