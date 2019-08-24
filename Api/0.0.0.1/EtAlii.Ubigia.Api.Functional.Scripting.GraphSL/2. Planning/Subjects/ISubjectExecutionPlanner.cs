namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    internal interface ISubjectExecutionPlanner : ISequencePartExecutionPlanner
    {
        ISubjectExecutionPlan Plan(SequencePart part);
    }
}