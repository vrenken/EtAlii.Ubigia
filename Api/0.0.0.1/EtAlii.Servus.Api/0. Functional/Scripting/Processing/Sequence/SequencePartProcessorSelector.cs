namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class SequencePartProcessorSelector : Selector<SequencePart, ISequencePartProcessor>, ISequencePartProcessorSelector
    {
        internal SequencePartProcessorSelector(
            IOperatorsProcessor operatorsProcessor,
            ISubjectsProcessor subjectsProcessor,
            ICommentProcessor commentProcessor)
        {
            this.Register(part => part is Operator, operatorsProcessor)
                .Register(part => part is Subject, subjectsProcessor)
                .Register(part => part is Comment, commentProcessor);
        }
    }
}
