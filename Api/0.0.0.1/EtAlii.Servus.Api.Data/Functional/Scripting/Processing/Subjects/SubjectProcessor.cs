namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class SubjectProcessor : ISequencePartProcessor
    {
        private readonly ISelector<Subject, ISubjectProcessor> _selector;


        internal SubjectProcessor(ISelector<Subject, ISubjectProcessor> selector)
        {
            _selector = selector;
        }

        public object Process(ProcessParameters<SequencePart, SequencePart> parameters)
        {
            var subject = parameters.Target as Subject;
            var processor = _selector.Select(subject);

            var subjectParameters = new ProcessParameters<Subject, SequencePart>(subject)
            {
                FuturePart = parameters.FuturePart,
                LeftPart = parameters.LeftPart,
                RightPart = parameters.RightPart,
                RightResult = parameters.RightResult,
                LeftResult = parameters.LeftResult,
            };
            return processor.Process(subjectParameters);
        }
    }
}
