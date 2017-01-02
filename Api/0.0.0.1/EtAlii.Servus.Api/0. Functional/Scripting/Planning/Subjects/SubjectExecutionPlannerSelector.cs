namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class SubjectExecutionPlannerSelector : ISubjectExecutionPlannerSelector
    {
        private readonly ISelector<Subject, ISubjectExecutionPlanner> _selector;

        public SubjectExecutionPlannerSelector(
            IPathSubjectExecutionPlanner pathSubjectExecutionPlanner,
            IConstantSubjectExecutionPlanner constantSubjectExecutionPlanner,
            IVariableSubjectExecutionPlanner variableSubjectExecutionPlanner,
            IFunctionSubjectExecutionPlanner functionSubjectExecutionPlanner)
        {
            _selector = new Selector<Subject, ISubjectExecutionPlanner>()
                .Register(subject => subject is PathSubject, pathSubjectExecutionPlanner)
                .Register(subject => subject is ConstantSubject, constantSubjectExecutionPlanner)
                .Register(subject => subject is VariableSubject, variableSubjectExecutionPlanner)
                .Register(subject => subject is FunctionSubject, functionSubjectExecutionPlanner);
        }

        public IExecutionPlanner Select(object item)
        {
            var subject = (Subject) item;
            return _selector.Select(subject);
        }
    }
}