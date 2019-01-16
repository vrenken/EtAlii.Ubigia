namespace EtAlii.Ubigia.Api.Functional
{
    using EtAlii.xTechnology.Structure;

    internal class ExecutionPlanCombinerSelector : IExecutionPlanCombinerSelector
    {
        private readonly ISelector<IExecutionPlanner, IExecutionPlanCombiner> _selector;

        public ExecutionPlanCombinerSelector(
            ISubjectExecutionPlanCombiner subjectExecutionPlanCombiner,
            IOperatorExecutionPlanCombiner operatorExecutionPlanCombiner)
        {
            _selector = new Selector<IExecutionPlanner, IExecutionPlanCombiner>()
                .Register(planner => planner is ISubjectExecutionPlanner, subjectExecutionPlanCombiner)
                .Register(planner => planner is IOperatorExecutionPlanner, operatorExecutionPlanCombiner);
        }

        public IExecutionPlanCombiner Select(IExecutionPlanner planner)
        {
            return _selector.Select(planner);
        }
    }
}