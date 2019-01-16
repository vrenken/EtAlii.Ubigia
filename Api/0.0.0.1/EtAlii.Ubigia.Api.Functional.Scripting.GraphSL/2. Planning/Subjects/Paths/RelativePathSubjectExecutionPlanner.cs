namespace EtAlii.Ubigia.Api.Functional
{
    internal class RelativePathSubjectExecutionPlanner : IRelativePathSubjectExecutionPlanner
    {
        private readonly IRelativePathSubjectProcessor _processor;

        public RelativePathSubjectExecutionPlanner(IRelativePathSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (RelativePathSubject)part;
            return new RelativePathSubjectExecutionPlan(pathSubject, _processor);
        }
    }
}