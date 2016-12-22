namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class SubjectExecutionPlannerSelector : ISubjectExecutionPlannerSelector
    {
        private readonly ISelector<Subject, ISubjectExecutionPlanner> _selector;

        public SubjectExecutionPlannerSelector(
            INonRootedPathSubjectExecutionPlanner nonRootedPathSubjectExecutionPlanner,
            IRootedPathSubjectExecutionPlanner rootedPathSubjectExecutionPlanner,
            IConstantSubjectExecutionPlanner constantSubjectExecutionPlanner,
            IVariableSubjectExecutionPlanner variableSubjectExecutionPlanner,
            IFunctionSubjectExecutionPlanner functionSubjectExecutionPlanner,
            IRootSubjectExecutionPlanner rootSubjectExecutionPlanner,
            IRootDefinitionSubjectExecutionPlanner rootDefinitionSubjectExecutionPlanner)
        {
            _selector = new Selector<Subject, ISubjectExecutionPlanner>()
                .Register(subject => subject is NonRootedPathSubject, nonRootedPathSubjectExecutionPlanner)
                .Register(subject => subject is RootedPathSubject, rootedPathSubjectExecutionPlanner)
                .Register(subject => subject is ConstantSubject, constantSubjectExecutionPlanner)
                .Register(subject => subject is VariableSubject, variableSubjectExecutionPlanner)
                .Register(subject => subject is FunctionSubject, functionSubjectExecutionPlanner)
                .Register(subject => subject is RootSubject, rootSubjectExecutionPlanner)
                .Register(subject => subject is RootDefinitionSubject, rootDefinitionSubjectExecutionPlanner);
        }

        public IExecutionPlanner Select(object item)
        {
            var subject = (Subject) item;
            return _selector.Select(subject);
        }
    }
}