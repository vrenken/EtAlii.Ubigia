namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using System.Threading.Tasks;
    using EtAlii.xTechnology.Structure;

    internal class ConstantSubjectsProcessor : IConstantSubjectsProcessor
    {
        private readonly ISelector<ConstantSubject, IConstantSubjectProcessor> _selector;

        public ConstantSubjectsProcessor(ISelector<ConstantSubject, IConstantSubjectProcessor> selector)
        {
            _selector = selector;
        }

        public async Task Process(Subject subject, ExecutionScope scope, IObserver<object> output)
        {
            var constantSubject = (ConstantSubject)subject;
            var processor = _selector.Select(constantSubject);

            //var constantSubjectParameters = new ProcessParameters<Subject, SequencePart>(constantSubject)
            //{
            //    FuturePart = parameters.FuturePart,
            //    LeftPart = parameters.LeftPart,
            //    RightPart = parameters.RightPart,
            //    RightResult = parameters.RightResult,
            //    LeftResult = parameters.LeftResult,
            //};
            await processor.Process(constantSubject, scope, output);
        }
    }
}
