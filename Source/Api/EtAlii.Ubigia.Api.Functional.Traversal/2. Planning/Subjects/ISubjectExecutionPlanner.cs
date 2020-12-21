namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal interface ISubjectExecutionPlanner : ISequencePartExecutionPlanner
    {
        ISubjectExecutionPlan Plan(SequencePart part);
    }
}
