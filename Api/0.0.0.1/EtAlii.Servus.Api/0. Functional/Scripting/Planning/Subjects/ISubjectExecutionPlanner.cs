namespace EtAlii.Servus.Api.Functional
{
    internal interface ISubjectExecutionPlanner : ISequencePartExecutionPlanner
    {
        ISubjectExecutionPlan Plan(SequencePart part);
    }
}