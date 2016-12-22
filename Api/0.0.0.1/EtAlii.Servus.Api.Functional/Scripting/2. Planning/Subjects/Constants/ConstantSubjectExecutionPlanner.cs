namespace EtAlii.Servus.Api.Functional
{
    internal class ConstantSubjectExecutionPlanner : IConstantSubjectExecutionPlanner
    {
        private readonly IConstantSubjectProcessorSelector _processorSelector;

        public ConstantSubjectExecutionPlanner(IConstantSubjectProcessorSelector processorSelector)
        {
            _processorSelector = processorSelector;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var constantSubject = (ConstantSubject)part;

            var processor = _processorSelector.Select(constantSubject);
            return new ConstantSubjectExecutionPlan(constantSubject, processor);
        }
    }
}