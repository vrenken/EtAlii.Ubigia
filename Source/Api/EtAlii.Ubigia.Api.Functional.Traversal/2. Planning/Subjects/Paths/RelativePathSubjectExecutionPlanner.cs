namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    internal class RelativePathSubjectExecutionPlanner : IRelativePathSubjectExecutionPlanner
    {
        private readonly IScriptProcessingContext _processingContext;

        public RelativePathSubjectExecutionPlanner(IScriptProcessingContext processingContext)
        {
            _processingContext = processingContext;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (RelativePathSubject)part;
            return new RelativePathSubjectExecutionPlan(pathSubject, _processingContext.RelativePathSubjectProcessor);
        }
    }
}
