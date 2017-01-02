namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.Servus.Api.Data.Model;
    using EtAlii.xTechnology.Structure;

    internal class SequencePartProcessorSelector : Selector<SequencePart, ISequencePartProcessor>
    {
        internal SequencePartProcessorSelector(
            OperatorProcessor operatorProcessor,
            SubjectProcessor subjectProcessor,
            CommentProcessor commentProcessor)
        {
            this.Register(part => part is Operator, operatorProcessor)
                .Register(part => part is Subject, subjectProcessor)
                .Register(part => part is Comment, commentProcessor);
        }
    }
}
