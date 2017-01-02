namespace EtAlii.Servus.Api.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using EtAlii.xTechnology.Structure;

    internal class ConstantSubjectProcessor : ISubjectProcessor
    {
        private readonly ISelector<ConstantSubject, IConstantSubjectProcessor> _selector;

        public ConstantSubjectProcessor(ISelector<ConstantSubject, IConstantSubjectProcessor> selector)
        {
            _selector = selector;
        }

        public object Process(ProcessParameters<Subject, SequencePart> parameters)
        {
            var constantSubject = (ConstantSubject)parameters.Target;
            var processor = _selector.Select(constantSubject);

            var constantSubjectParameters = new ProcessParameters<ConstantSubject, SequencePart>(constantSubject)
            {
                FuturePart = parameters.FuturePart,
                LeftPart = parameters.LeftPart,
                RightPart = parameters.RightPart,
                RightResult = parameters.RightResult,
                LeftResult = parameters.LeftResult,
            };
            return processor.Process(constantSubjectParameters);
        }
    }
}
