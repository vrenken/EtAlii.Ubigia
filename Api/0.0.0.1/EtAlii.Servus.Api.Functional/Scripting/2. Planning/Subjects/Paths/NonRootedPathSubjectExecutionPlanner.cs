namespace EtAlii.Servus.Api.Functional
{
    internal class NonRootedPathSubjectExecutionPlanner : INonRootedPathSubjectExecutionPlanner
    {
        private readonly INonRootedPathSubjectProcessor _processor;

        public NonRootedPathSubjectExecutionPlanner(INonRootedPathSubjectProcessor processor)
        {
            _processor = processor;
        }

        public ISubjectExecutionPlan Plan(SequencePart part)
        {
            var pathSubject = (NonRootedPathSubject)part;
            return new NonRootedPathSubjectExecutionPlan(pathSubject, _processor);
        }
    }
}