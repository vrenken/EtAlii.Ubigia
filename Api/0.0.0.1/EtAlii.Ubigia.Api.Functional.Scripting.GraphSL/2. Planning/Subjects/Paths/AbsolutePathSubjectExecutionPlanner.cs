namespace EtAlii.Ubigia.Api.Functional
{
    internal class AbsolutePathSubjectExecutionPlanner : IAbsolutePathSubjectExecutionPlanner
    {
        private readonly IProcessingContext _processingContext;

        public AbsolutePathSubjectExecutionPlanner(IProcessingContext processingContext)
        {
            _processingContext = processingContext;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (AbsolutePathSubject)part;
            return new AbsolutePathSubjectExecutionPlan(pathSubject, _processingContext.AbsolutePathSubjectProcessor);
        }
    }
}