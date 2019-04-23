namespace EtAlii.Ubigia.Api.Functional
{
    internal class RootedPathSubjectExecutionPlanner : IRootedPathSubjectExecutionPlanner
    {
        private readonly IProcessingContext _processingContext;

        public RootedPathSubjectExecutionPlanner(IProcessingContext processingContext)
        {
            _processingContext = processingContext;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (RootedPathSubject)part;
            return new RootedPathSubjectExecutionPlan(pathSubject, _processingContext.RootedPathSubjectProcessor);
        }
    }
}