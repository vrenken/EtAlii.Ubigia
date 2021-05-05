namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class RootedPathSubjectExecutionPlanner : IRootedPathSubjectExecutionPlanner
    {
        private readonly IScriptProcessingContext _processingContext;

        public RootedPathSubjectExecutionPlanner(IScriptProcessingContext processingContext)
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
