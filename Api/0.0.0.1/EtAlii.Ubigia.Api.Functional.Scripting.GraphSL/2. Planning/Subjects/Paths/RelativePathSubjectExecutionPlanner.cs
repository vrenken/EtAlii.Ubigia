namespace EtAlii.Ubigia.Api.Functional
{
    internal class RelativePathSubjectExecutionPlanner : IRelativePathSubjectExecutionPlanner
    {
        private readonly IProcessingContext _processingContext;

        public RelativePathSubjectExecutionPlanner(IProcessingContext processingContext)
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