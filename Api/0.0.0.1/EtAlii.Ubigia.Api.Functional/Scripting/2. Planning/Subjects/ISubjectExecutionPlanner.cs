namespace EtAlii.Ubigia.Api.Functional
{
    internal interface ISubjectExecutionPlanner : ISequencePartExecutionPlanner
    {
        ISubjectExecutionPlan Plan(SequencePart part);
    }
}